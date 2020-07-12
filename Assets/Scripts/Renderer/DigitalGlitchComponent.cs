
using System;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace UnityEngine.Experimental.Rendering.HDPipeline
{
    [Serializable, VolumeComponentMenu("Post-processing/Digital Glitch")]
    public sealed class DigitalGlitchComponent : VolumeComponent, IPostProcessComponent
    {
        public BoolParameter displayDiffuseOnly = new BoolParameter(false);

        [Tooltip("View normals in World Space")]
        public BoolParameter displayNormalsWS = new BoolParameter(false);

        [Tooltip("View normals in View Space")]
        public BoolParameter displayNormalsVS = new BoolParameter(false);

        [Tooltip("View normals in World Space")]
        public BoolParameter displayGeometricNormalsWS = new BoolParameter(false);

        [Tooltip("View normals in View Space")]
        public BoolParameter displayGeometricNormalsVS = new BoolParameter(false);

        public BoolParameter displayDepth = new BoolParameter(false);
        public BoolParameter displayRoughness = new BoolParameter(false);

        [Tooltip("Specifies color of the outline")]
        public ColorParameter outlineColor = new ColorParameter(new Color(0, 0, 0, 0), false, true, true);

        public override void Override(VolumeComponent state, float interpFactor)
        {

        }

        public bool IsActive()
        {
            return outlineColor.value.a > 0f;
        }

        public bool IsTileCompatible()
        {
            throw new NotImplementedException();
        }
    }
}
