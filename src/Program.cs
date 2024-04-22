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


var video = await new YoutubeClient().Videos.GetAsync(urlString);
var streamManifest = await new YoutubeClient().Videos.Streams.GetManifestAsync(video.Id);
var streamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();

Console.WriteLine("Downloading Audio");
var fileLocation = Path.Combine(Environment.CurrentDirectory, $"{video.Id}.{streamInfo.Container.Name}");
await new YoutubeClient().Videos.Streams.DownloadAsync(streamInfo, fileLocation);
Console.WriteLine("Done!");
Console.WriteLine($"File Location: {fileLocation}");

return 0;