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
I'm so happy with how clean it looks! Even though it isn't that complex, I'm still very satisfied with how polished and reactive the design is!

# Proposal submitted earlier can go here:
I'm creating a beautiful audio visualizer that will almost seem like it's floating through space. It will consist of a user's desired
amount of spheres that will scale, rotate, and move based on the song that is playing.

https://github.com/KxngBari/C18416902GEAssignment

## This is how to markdown text:

This is *emphasis*

This is a bulleted list

- Item
- Item

This is a numbered list

1. Item
1. Item

This is a [hyperlink](http://bryanduggan.org)

# Headings
## Headings
#### Headings
##### Headings

This is code:

```Java
public void render()
{
	ui.noFill();
	ui.stroke(255);
	ui.rect(x, y, width, height);
	ui.textAlign(PApplet.CENTER, PApplet.CENTER);
	ui.text(text, x + width * 0.5f, y + height * 0.5f);
}
```

So is this without specifying the language:

```
public void render()
{
	ui.noFill();
	ui.stroke(255);
	ui.rect(x, y, width, height);
	ui.textAlign(PApplet.CENTER, PApplet.CENTER);
	ui.text(text, x + width * 0.5f, y + height * 0.5f);
}
```

This is an image using a relative URL:

![An image](images/p8.png)

This is an image using an absolute URL:

![A different image](https://bryanduggandotorg.files.wordpress.com/2019/02/infinite-forms-00045.png?w=595&h=&zoom=2)

This is a youtube video:

[![YouTube](http://img.youtube.com/vi/J2kHSSFA4NU/0.jpg)](https://www.youtube.com/watch?v=J2kHSSFA4NU)

This is a table:

| Heading 1 | Heading 2 |
|-----------|-----------|
|Some stuff | Some more stuff in this column |
|Some stuff | Some more stuff in this column |
|Some stuff | Some more stuff in this column |
|Some stuff | Some more stuff in this column |

