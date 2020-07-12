using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Random = Unity.Mathematics.Random;
using Unity.Mathematics;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Kino Image Effects/Digital Glitch")]
public sealed class DigitalGlitchEffect : MonoBehaviour
{
    // Inspector values
    [Range(0f, 1f)]
    [SerializeField] float Intensity = 0f;
    [SerializeField] Material Material = default;

    // Tag
    readonly string BLIT_PASS_TAG = "DigitalGlitchEffect";

    // Material Properties
    static readonly int MainTexID = Shader.PropertyToID("_MainTex");
    static readonly int NoiseTexID = Shader.PropertyToID("_NoiseTex");
    static readonly int TrashTexID = Shader.PropertyToID("_TrashTex");
    static readonly int IntensityID = Shader.PropertyToID("_Intensity");

    Random random;
    Texture2D noiseTexture;
    RenderTexture mainTexture;
    RenderTexture trashFrame1;
    RenderTexture trashFrame2;
    Material materialInstance;

    Material MaterialInstance
    {
        get
        {
            if (materialInstance == null)
            {
                materialInstance = Instantiate(Material);
            }

            return materialInstance;
        }
    }

    Color RandomColor
    {
        get
        {
            var r = random.NextFloat4();
            return new Color(r.x, r.y, r.z, r.w);
        }
    }

    void OnEnable()
        => RenderPipelineManager.beginCameraRendering += OnCameraRendering;


    void OnDisable()
        => RenderPipelineManager.beginCameraRendering -= OnCameraRendering;

    void Awake()
    {
        random = new Random((uint)System.DateTime.Now.Ticks);
        SetUpResources();
        UpdateNoiseTexture();
    }

    void SetUpResources()
    {
        noiseTexture = new Texture2D(64, 32, TextureFormat.ARGB32, false);
        noiseTexture.hideFlags = HideFlags.DontSave;
        noiseTexture.wrapMode = TextureWrapMode.Clamp;
        noiseTexture.filterMode = FilterMode.Point;

        trashFrame1 = new RenderTexture(Screen.width, Screen.height, 0);
        trashFrame2 = new RenderTexture(Screen.width, Screen.height, 0);
        mainTexture = new RenderTexture(Screen.width, Screen.height, 0);
        trashFrame1.hideFlags = HideFlags.DontSave;
        trashFrame2.hideFlags = HideFlags.DontSave;
        mainTexture.hideFlags = HideFlags.DontSave;
    }

    void UpdateNoiseTexture()
    {
        var color = RandomColor;
        for (var y = 0; y < noiseTexture.height; y++)
        {
            for (var x = 0; x < noiseTexture.width; x++)
            {
                if (random.NextFloat() > 0.89f)
                {
                    color = RandomColor;
                }

                noiseTexture.SetPixel(x, y, color);
            }
        }
        noiseTexture.Apply();
    }


    void OnCameraRendering(ScriptableRenderContext context, Camera camera)
    {
        if (camera != Camera.main) return;
        var material = MaterialInstance;
        if (material == null) return;

        if (random.NextFloat() > math.lerp(0.9f, 0.5f, Intensity))
        {
            UpdateNoiseTexture();
        }

        var cmd = CommandBufferPool.Get(BLIT_PASS_TAG);

        // Commander
        var activeTexture = camera.activeTexture;
        cmd.Blit(activeTexture, mainTexture);

        // Update trash frames on a constant interval.
        var frameCount = Time.frameCount;
        if (frameCount % 13 == 0) cmd.Blit(activeTexture, trashFrame1);
        if (frameCount % 73 == 0) cmd.Blit(activeTexture, trashFrame2);

        // Materialに必要な情報を渡しつつ書き込み.
        material.SetFloat(IntensityID, Intensity);
        material.SetTexture(MainTexID, mainTexture);
        material.SetTexture(NoiseTexID, noiseTexture);
        material.SetTexture(TrashTexID, random.NextFloat() > 0.5f ? trashFrame1 : trashFrame2);
        cmd.Blit(mainTexture, camera.activeTexture, material);

        // CommandBufferの実行.
        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }

}
