using UnityEditor;
using UnityEngine;
using System.IO;

public class AudioPostProcessor : AssetPostprocessor
{
    private const long MIN_SIZE_TRESHOLD = 200;
    private const long MUSIC_SIZE_TRESHOLD = 5120;
    
    public class MyAudioPostprocessor : AssetPostprocessor
    {
        private void OnPreprocessAudio()
        {
            AudioImporter audioImporter = (AudioImporter)assetImporter;
            AudioImporterSampleSettings sampleSettings = audioImporter.defaultSampleSettings;

            FileInfo f = new FileInfo(assetPath);
            long size = f.Length / 1024; // size in kb

            if (size < MIN_SIZE_TRESHOLD)
            {
                sampleSettings.loadType = AudioClipLoadType.DecompressOnLoad;
                sampleSettings.compressionFormat = AudioCompressionFormat.ADPCM;
            }
            else if (size > MIN_SIZE_TRESHOLD)
            {
                if (size < MUSIC_SIZE_TRESHOLD)
                {
                    // if file size is less than 5mb -> sound effect                    
                    sampleSettings.loadType = AudioClipLoadType.CompressedInMemory;
                    sampleSettings.compressionFormat = AudioCompressionFormat.ADPCM;
                }
                else
                {
                    // if file size is greater than 5mb -> background music
                    sampleSettings.loadType = AudioClipLoadType.Streaming;
                    sampleSettings.compressionFormat = AudioCompressionFormat.Vorbis;
                    sampleSettings.quality = 0.5f;
                }
            }
            audioImporter.defaultSampleSettings = sampleSettings;
        }
    }
}
