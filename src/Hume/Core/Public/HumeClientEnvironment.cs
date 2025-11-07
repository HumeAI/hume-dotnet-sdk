namespace Hume;

[Serializable]
public class HumeClientEnvironment
{
    public static readonly HumeClientEnvironment Prod = new HumeClientEnvironment
    {
        Base = "https://api.hume.ai",
        Evi = "wss://api.hume.ai/v0/evi",
        Tts = "wss://api.hume.ai/v0/tts",
        Stream = "wss://api.hume.ai/v0/stream",
    };

    /// <summary>
    /// URL for the Base service
    /// </summary>
    public string Base { get;
#if NET5_0_OR_GREATER
        init;
#else
        set;
#endif
    }

    /// <summary>
    /// URL for the evi service
    /// </summary>
    public string Evi { get;
#if NET5_0_OR_GREATER
        init;
#else
        set;
#endif
    }

    /// <summary>
    /// URL for the tts service
    /// </summary>
    public string Tts { get;
#if NET5_0_OR_GREATER
        init;
#else
        set;
#endif
    }

    /// <summary>
    /// URL for the stream service
    /// </summary>
    public string Stream { get;
#if NET5_0_OR_GREATER
        init;
#else
        set;
#endif
    }
}
