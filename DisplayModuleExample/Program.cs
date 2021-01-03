using System;
using Microsoft.SPOT;
using CTRE.Gadgeteer.Module;
using CTRE.Phoenix.Controller;
using CTRE.Phoenix;

namespace DisplayModuleExample
{
    public class Program
    {
        public static void Main()
        {
            // Game Controller
            GameController gamepad = new GameController(UsbHostDevice.GetInstance());

            // NinaB Font
            Font ninaB = Properties.Resources.GetFont(Properties.Resources.FontResources.NinaB);

            // Initializing a display module: DisplayModule(port, orientation)
            DisplayModule displayModule = new DisplayModule(CTRE.HERO.IO.Port8, DisplayModule.OrientationType.Landscape);

            while (true)
            {
                // Connect the game controller first so that the sprites show up
                if (gamepad.GetConnectionStatus() == UsbDeviceConnection.Connected)
                {
                    // Erases everything on the display
                    displayModule.Clear();

                    // Adding labels: [Display Module Name].AddLabelSprite(font, colour, x_pos, y_pos, width, height)
                    DisplayModule.LabelSprite title = displayModule.AddLabelSprite(ninaB, DisplayModule.Color.White, 27, 17, 120, 15);
                    DisplayModule.LabelSprite x_label = displayModule.AddLabelSprite(ninaB, DisplayModule.Color.White, 80, 65, 80, 15);
                    DisplayModule.LabelSprite y_label = displayModule.AddLabelSprite(ninaB, DisplayModule.Color.White, 80, 85, 80, 15);

                    // Adding rectangles: [Display Module Name].AddRectSprite(colour, x_pos, y_pos, width, height)
                    DisplayModule.RectSprite x_rect = displayModule.AddRectSprite(DisplayModule.Color.White, 20, 55, 18, 55);
                    DisplayModule.RectSprite y_rect = displayModule.AddRectSprite(DisplayModule.Color.White, 47, 55, 18, 55);
                    
                    // Everything gets cleared when the game controller is unplugged
                    while (gamepad.GetConnectionStatus() == UsbDeviceConnection.Connected)
                    {
                        // Declares and resets the joystick
                        double x_value = gamepad.GetAxis(0);
                        double y_value = -gamepad.GetAxis(1);
                        if (x_value < 0.05 && x_value > -0.05)
                        {
                            x_value = 0;
                        }
                        if (y_value < 0.05 && y_value > -0.05)
                        {
                            y_value = 0;
                        }

                        // Changes the color of the rectangle (x-value of the left joystick): [Rectangle Name].SetColor(colour)
                        if (x_value > 0.05)
                        {
                            x_rect.SetColor(DisplayModule.Color.Green);
                        }
                        else if (x_value < -0.05)
                        {
                            x_rect.SetColor(DisplayModule.Color.Red);
                        }
                        else
                        {
                            x_rect.SetColor(DisplayModule.Color.White);
                        }

                        // Changes the color of the rectangle (y-value of the left joystick): [Rectangle Name].SetColor(colour)
                        if (y_value > 0.05)
                        {
                            y_rect.SetColor(DisplayModule.Color.Green);
                        }
                        else if (y_value < -0.05)
                        {
                            y_rect.SetColor(DisplayModule.Color.Red);
                        }
                        else
                        {
                            y_rect.SetColor(DisplayModule.Color.White);
                        }

                        // Sets the text that the label displays: [Label Name].SetText(text: string)
                        title.SetText("Joystick Control");
                        x_label.SetText("X: " + x_value.ToString());
                        y_label.SetText("Y: " + y_value.ToString());
                    }
                }
                else
                {
                    // Erases everything on the display
                    displayModule.Clear();

                    // Adding images: [Display Module Name].AddResourceImageSprite(resource_manager, img_ID, img_type, x_pos, y_pos)
                    DisplayModule.ResourceImageSprite image = displayModule.AddResourceImageSprite(Properties.Resources.ResourceManager, Properties.Resources.BinaryResources.img, Bitmap.BitmapImageType.Jpeg, 44, 16);

                    // Adding labels: [Display Module Name].AddLabelSprite(font, colour, x_pos, y_pos, width, height)
                    DisplayModule.LabelSprite text = displayModule.AddLabelSprite(ninaB, DisplayModule.Color.White, 36, 99, 100, 30);
                    
                    // Sets the text that the label displays: [Label Name].SetText(text: string)
                    text.SetText("TAS Robotics");

                    // Keeps the image and text while the gamepad is unplugged
                    while (gamepad.GetConnectionStatus() == UsbDeviceConnection.NotConnected)
                    {
                    }
                }
                System.Threading.Thread.Sleep(100);
            }
        }
    }
}