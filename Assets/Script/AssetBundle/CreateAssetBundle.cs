#if UNITY_EDITOR
using UnityEditor;
using System.IO;

public class CreateAssetBundles
{
    private const string CODE = @"

public enum AssetsName
{
    armor,
    arrow,
    boots,
    bow,
    glove,
    helmet,
    weapon,
    weaponObj,
}
";

    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        string assetBundleDirectory = "Assets/AssetBundles";
        if (!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }
        BuildPipeline.BuildAssetBundles(assetBundleDirectory,
                                        BuildAssetBundleOptions.None,
                                        BuildTarget.StandaloneWindows);

        var enumPath = "Assets/Script/AssetBundle/AssetsName.cs";

        File.WriteAllText(enumPath, CODE);

        AssetDatabase.Refresh();
    }

}
#endif