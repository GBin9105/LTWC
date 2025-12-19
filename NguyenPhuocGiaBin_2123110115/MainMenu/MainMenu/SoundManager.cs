using System;
using System.IO;
using NAudio.Wave;

namespace MainMenu
{
    public static class SoundManager
    {
        private static WaveOutEvent bgOutput;
        private static AudioFileReader bgReader;
        private static bool isStopping = false;

        public static void PlayBackground(string fileName)
        {
            StopBackground();
            isStopping = false;

            string path = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Sounds",
                fileName
            );

            bgReader = new AudioFileReader(path);
            bgOutput = new WaveOutEvent();
            bgOutput.Init(bgReader);

            bgOutput.PlaybackStopped += BgPlaybackStopped;
            bgOutput.Play();
        }

        private static void BgPlaybackStopped(object sender, StoppedEventArgs e)
        {
            if (isStopping) return;
            if (bgReader == null || bgOutput == null) return;

            bgReader.Position = 0;
            bgOutput.Play();
        }

        public static void PlayEffect(string fileName)
        {
            string path = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Sounds",
                fileName
            );

            var reader = new AudioFileReader(path);
            var output = new WaveOutEvent();
            output.Init(reader);
            output.Play();

            output.PlaybackStopped += (s, e) =>
            {
                output.Dispose();
                reader.Dispose();
            };
        }

        public static void StopBackground()
        {
            isStopping = true;

            if (bgOutput != null)
            {
                bgOutput.PlaybackStopped -= BgPlaybackStopped;
                bgOutput.Stop();
                bgOutput.Dispose();
                bgOutput = null;
            }

            if (bgReader != null)
            {
                bgReader.Dispose();
                bgReader = null;
            }
        }
    }
}
