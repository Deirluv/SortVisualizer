using System.Threading.Tasks;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace SortVisualizer.Infrastructure;

public class AudioService
{
    private readonly WaveOutEvent _waveOut;
    private readonly SignalGenerator _signalGenerator;

    public AudioService()
    {
        _waveOut = new WaveOutEvent();
        _signalGenerator = new SignalGenerator
        {
            Gain = 0.1,
            Type = SignalGeneratorType.Sin
        };
        _waveOut.Init(_signalGenerator);
    }

    public void PlayTone(int value)
    {
        double frequency = 200 + (value * 4); 
        _signalGenerator.Frequency = frequency;

        if (_waveOut.PlaybackState != PlaybackState.Playing)
        {
            _waveOut.Play();
        }
        
        Task.Delay(20).ContinueWith(_ => Stop());
    }

    public void Stop() => _waveOut.Stop();
}