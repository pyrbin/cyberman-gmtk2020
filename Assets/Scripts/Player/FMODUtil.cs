using System;
using FMODUnity;
using Unity.Mathematics;

static class FMODUtil
{
    public static void PlayOneShot(string path, string parameterName, float parameterValue, float3 position = new float3())
    {
        PlayOneShot(FMODUnity.RuntimeManager.PathToGUID(path), parameterName, parameterValue, position);
    }

    public static void PlayOneShot(Guid guid, string parameterName, float parameterValue, float3 position = new float3())
    {
        var instance = FMODUnity.RuntimeManager.CreateInstance(guid);
        instance.set3DAttributes(RuntimeUtils.To3DAttributes(position));
        instance.setParameterByName(parameterName, parameterValue);
        instance.start();
        instance.release();
    }
}
