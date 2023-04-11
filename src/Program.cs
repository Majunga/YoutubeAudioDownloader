using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

if (args.Length == 0)
{
    Console.WriteLine("Youtube url required");
    return 1;
}

var urlString = args[0];
if(Uri.TryCreate(urlString, UriKind.Absolute, out var url) == false)
{
    Console.WriteLine($"Invalid Url: {urlString}");
    return 1;
}

var youtoobs = new YoutubeClient();

var video = await youtoobs.Videos.GetAsync(urlString);
var streamManifest = await youtoobs.Videos.Streams.GetManifestAsync(video.Id);
var streamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();

await youtoobs.Videos.Streams.DownloadAsync(streamInfo, $"{Environment.CurrentDirectory}/{video.Id}.{streamInfo.Container.Name}");

return 0;