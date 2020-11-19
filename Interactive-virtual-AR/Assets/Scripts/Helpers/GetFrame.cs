using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using GoogleARCore;
using System.IO;

public class GetFrame : MonoBehaviour
{
    private static GetFrame instance;

    public static GetFrame Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<GetFrame>();
            }
            return instance;
        }
    }

    public byte[] GetImage()
    {
        var image = Frame.CameraImage.AcquireCameraImageBytes();

        byte[] bufferY = new byte[image.Width * image.Height];
        byte[] bufferU = new byte[image.Width * image.Height / 2];
        byte[] bufferV = new byte[image.Width * image.Height / 2];
        System.Runtime.InteropServices.Marshal.Copy(image.Y, bufferY, 0, image.Width * image.Height);
        System.Runtime.InteropServices.Marshal.Copy(image.U, bufferU, 0, image.Width * image.Height / 2);
        System.Runtime.InteropServices.Marshal.Copy(image.V, bufferV, 0, image.Width * image.Height / 2);


        Texture2D m_TextureRender = new Texture2D(image.Width, image.Height, TextureFormat.RGBA32, false, false);


        Color c = new Color();
        for (int y = 0; y < image.Height; y++) {
            for (int x =0; x<image.Width;x++) {
                float Y = bufferY[y * image.Width + x];
                float U = bufferU[(y/2) * image.Width + x];
                float V = bufferV[(y/2) * image.Width + x];
                c.r = Y;
                c.g = Y;
                c.b = Y;

                c.r /= 255.0f;
                c.g /= 255.0f;
                c.b /= 255.0f;

                if (c.r < 0.0f) c.r = 0.0f;
                if (c.g < 0.0f) c.g = 0.0f;
                if (c.b < 0.0f) c.b = 0.0f;

                if (c.r > 1.0f) c.r = 1.0f;
                if (c.g > 1.0f) c.g = 1.0f;
                if (c.b > 1.0f) c.b = 1.0f;

                c.a = 1.0f;
                m_TextureRender.SetPixel(image.Width-1-x, y, c);      
            }
        }

        byte[] encodedJpg = m_TextureRender.EncodeToJPG();
        return encodedJpg;
    }
}
