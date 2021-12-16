# Project Title
Audio Box

Name: Ire Adebari

Student Number:  C18416902

Class Group: TU857

# Description of the project
This is an audio visualizer that contains a generated number of spheres that scale, rotate, and move according to the different frequency bands in the music playing.

# Instructions for use
As long as you have the post-processing package installed, everything should run just fine!

# How it works
A cube is created which consists of several different lines. Each line represents a particle (in this case, the spheres you see). The spheres have a number of properties,
such as colour, movement speed, rotation speed, bloom, etc. When the music starts, each sphere is randomly assigned a spot on the frequency spectrum which will determine what
sounds in the music it should respond to. So for example, some spheres might only respond to the bass in the music. Whereas other spheres might only recognize the very high treble.

# List of classes/assets in the project and whether made yourself or modified or if its from a source, please give the reference
AudioBoxParticle.cs - Self written
AudioBoxSound.cs - Self written
AudioPlayer.cs - Self written
FastNoise.cs - From[https://github.com/Auburn/FastNoise_CSharp]

# References
Visualizing audio - https://www.youtube.com/watch?v=wtXirrO-iNA

# What I am most proud of in the assignment
I'm so happy with how clean it looks! Even though it isn't that complex, I'm still very satisfied with how polished and reactive the design is! Also, the track played in the project
is a track I produced for my friend who owns a circus, so I'm definitely proud I could put the song to use here as well!

# Proposal submitted earlier can go here:
I'm creating a beautiful audio visualizer that will almost seem like it's floating through space. It will consist of a user's desired
amount of spheres that will scale, rotate, and move based on the song that is playing.

https://github.com/KxngBari/GEAssignmentRepo2

This is code:

```C#
    void CreateAudioBands()
    {
        for (int i = 0; i < 8; i++)
        {
            if (_frequencyBands[i] > _frequencyBandHighest[i])
            {
                _frequencyBandHighest[i] = _frequencyBands[i];
            }
            _audioBand[i] = (_frequencyBands[i] / _frequencyBandHighest[i]);
            _audioBandBuffer[i] = (_bandBuffer[i] / _frequencyBandHighest[i]);
        }
    }
```

The above code assigns a frequency bad to each individual particle, going 0 to 7.

```C#
    void CalculateAudioBoxDirections()
    {
        _offset = new Vector3(_offset.x + (_offsetSpeed.x * Time.deltaTime), _offset.y + (_offsetSpeed.y * Time.deltaTime), _offset.z + (_offsetSpeed.z * Time.deltaTime));
        float xOff = 0f;
        for (int x = 0; x < _gridSize.x; x++)
        {
            float yOff = 0f;
            for (int y = 0; y < _gridSize.y; y++)
            {
                float zOff = 0f;
                for (int z = 0; z < _gridSize.z; z++)
                {
                    float noise = _fastNoise.GetSimplex(xOff + _offset.x, yOff + _offset.y, zOff + _offset.z) + 1;
                    Vector3 noiseDirection = new Vector3(Mathf.Cos(noise * Mathf.PI), Mathf.Sin(noise * Mathf.PI), Mathf.Cos(noise * Mathf.PI));
                    _audioBoxDirection[x, y, z] = Vector3.Normalize(noiseDirection);

                    zOff = zOff + _increment;
                }
                yOff = yOff + _increment;
            }
            xOff = xOff + _increment;
        }
    }
```
The above code calculates which direction the particles will move within the audio box.

!https://imgur.com/a/PzIAwJe