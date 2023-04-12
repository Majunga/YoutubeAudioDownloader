using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

var originalUrlString = string.Empty;
if (args.Length == 0)
{
    Console.Write("Enter Url:");
    originalUrlString = Console.ReadLine();
}
else if (originalUrlString == string.Empty && args.Length > 0)
{
    originalUrlString = args[0];
}

var urlString = originalUrlString ?? string.Empty;

if(urlString.StartsWith("https://") == false && urlString.StartsWith("http://") == false)
{
    urlString = $"https://{urlString}";
}

if (Uri.TryCreate(urlString, UriKind.Absolute, out var url) == false)
{
    Console.WriteLine($"Invalid Url: {originalUrlString}");
    return 1;
}

var youtoobs = new YoutubeClient();

var video = await youtoobs.Videos.GetAsync(urlString);
var streamManifest = await youtoobs.Videos.Streams.GetManifestAsync(video.Id);
var streamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();

Console.WriteLine("Downloading Audio");
await youtoobs.Videos.Streams.DownloadAsync(streamInfo, $"{Environment.CurrentDirectory}/{video.Id}.{streamInfo.Container.Name}");
Console.WriteLine("Done!");
return 0;