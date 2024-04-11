using ItemPrinterDeGacha.Core;

#if DEBUG

namespace ItemPrinterDeGacha.WinForms;

public static class DevUtil
{
    private static readonly string[] Languages = ["ja", "fr", "it", "de", "es", "ko", "zh", "zh2"];
    private const string DefaultLanguage = GameStrings.DefaultLanguage;

    public static bool IsUpdatingTranslations { get; private set; }

    private static string GetTextDir() => GetResourcePath("ItemPrinterDeGacha.WinForms", "Resources", "text");

    /// <summary>
    /// Call this to update all translatable resources (Program GUI)
    /// </summary>
    public static void UpdateAll()
    {
        if (IsUpdatingTranslations)
            return;
        IsUpdatingTranslations = true;
        UpdateTranslations();
        UpdateMessages();
        IsUpdatingTranslations = false;
    }

    private static void UpdateMessages()
    {
        var resultDir = GetTextDir();
        DumpMessageSettings(DefaultLanguage, resultDir);
        foreach (var language in Languages)
            DumpMessageSettings(language, resultDir);

        return;

        static void DumpMessageSettings(ReadOnlySpan<char> language, string resultDir)
        {
            var loc = Localization.Load(language);
            loc.LocalizeEnum<PrintMode>();
            loc.LocalizeEnum<SearchModeRegular>();
            loc.LocalizeEnum<SearchModeBall>();
            loc.Save(resultDir, language);
        }
    }

    private static void UpdateTranslations()
    {
        var assembly = System.Reflection.Assembly.GetExecutingAssembly();
        var types = assembly.GetTypes();
        // add mode
        WinFormsTranslator.SetRemovalMode(false);
        WinFormsTranslator.LoadAllForms(types, LoadBanlist); // populate with every possible control
        WinFormsTranslator.TranslateControls(GetExtraControls());
        WinFormsTranslator.UpdateAll(DefaultLanguage, Languages); // propagate to others
        WinFormsTranslator.DumpAll(Banlist); // dump current to file

        // de-populate
        WinFormsTranslator.SetRemovalMode(); // remove used keys, don't add any
        WinFormsTranslator.LoadAllForms(types, LoadBanlist);
        WinFormsTranslator.TranslateControls(GetExtraControls());
        WinFormsTranslator.RemoveAll(DefaultLanguage, PurgeBanlist); // remove all lines from above generated files that still remain

        // Move translated files from the debug exe loc to their project location
        var files = Directory.GetFiles(Application.StartupPath);
        var dir = GetTextDir();
        foreach (var f in files)
        {
            var fn = Path.GetFileName(f);
            if (!fn.EndsWith(".txt"))
                continue;
            if (!fn.StartsWith("lang_"))
                continue;

            var loc = Path.Combine(dir, fn);
            if (File.Exists(loc))
                File.Delete(loc);
            File.Move(f, loc, true);
        }

        Application.Exit();
    }

    private static IEnumerable<Control> GetExtraControls()
    {
        yield break;
    }

    private static readonly string[] LoadBanlist =
    [
    ];

    private static readonly string[] Banlist =
    [
        ..LoadBanlist,
    ];

    private static readonly string[] PurgeBanlist =
    [
    ];

    private static string GetResourcePath(params string[] subdir)
    {
        // Starting from the executable path, crawl upwards until we get to the repository/sln root
        const string repo = "ItemPrinterDeGacha";
        var path = Application.StartupPath;
        while (true)
        {
            var parent = Directory.GetParent(path) ?? throw new DirectoryNotFoundException(path);
            path = parent.FullName;
            if (path.EndsWith(repo))
                return Path.Combine(path, Path.Combine(subdir));
        }
    }
}
#endif
