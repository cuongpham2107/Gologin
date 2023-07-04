


public class Geolocation
{
    public string? mode { get; set; }
    public bool enabled { get; set; }
    public bool customize { get; set; }
    public bool fillBasedOnIp { get; set; }
    public int latitude { get; set; }
    public int longitude { get; set; }
    public int accuracy { get; set; }
}

public class Navigator
{
    public int hardwareConcurrency { get; set; }
    public bool doNotTrack { get; set; }
    public int deviceMemory { get; set; }
    public string? userAgent { get; set; }
    public string? resolution { get; set; }
    public string? language { get; set; }
    public string? platform { get; set; }
}

public class Proxy
{
    public string? mode { get; set; }
    public int port { get; set; }
    public string? host { get; set; }
    public string? username { get; set; }
    public string? password { get; set; }
    public string? id { get; set; }
}

public class Profile
{
    public string? name { get; set; }
    public string? role { get; set; }
    public string id { get; set; }
    public string? notes { get; set; }
    public string? browserType { get; set; }
    public bool lockEnabled { get; set; }
    public Timezone? timezone { get; set; }
    public Navigator? navigator { get; set; }
    public Geolocation? geolocation { get; set; }
    public bool debugMode { get; set; }
    public string? os { get; set; }
    public Proxy? proxy { get; set; }
    public List<object>? folders { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    public List<string>? chromeExtensions { get; set; }
    public List<object>? userChromeExtensions { get; set; }
    public List<object>? tags { get; set; }
    public bool proxyEnabled { get; set; }
    public bool isBookmarksSynced { get; set; }
    public bool autoLang { get; set; }
}

public class Timezone
{
    public bool enabled { get; set; }
    public bool fillBasedOnIp { get; set; }
    public string? timezone { get; set; }
}