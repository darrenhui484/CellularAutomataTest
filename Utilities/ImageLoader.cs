using System;
using Godot;

public static class ImageLoader {
    public static ImageTexture LoadImage(string filepath) {
        Image image = new Image();
        Error error = image.Load(filepath);
        if (error != Error.Ok) {
            throw new InvalidOperationException("Issue importing image");
        }

        ImageTexture imageTexture = new ImageTexture();
        imageTexture.CreateFromImage(image);
        return imageTexture;
    }
}