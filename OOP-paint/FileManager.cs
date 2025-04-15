using Newtonsoft.Json;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using OOP_paint.ShapeModels;

namespace OOP_paint
{
    public class FileManager
    {
        public void SaveFile(List<ShapeBase> shapes)
        {
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Filter = "JSON Files (*.json)|*.json",
                DefaultExt = "json"
            };

            if (saveDialog.ShowDialog() == true)
            {
                try
                {
                    var settings = new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All,
                        Formatting = Formatting.Indented,
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    };

                    string json = JsonConvert.SerializeObject(shapes, settings);
                    File.WriteAllText(saveDialog.FileName, json);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при сохранении: " + ex.Message);
                }
            }
        }

        public void OpenFile(ref List<ShapeBase> shapes)
        {
            OpenFileDialog openDialog = new OpenFileDialog
            {
                Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*"
            };

            if (openDialog.ShowDialog() == true)
            {
                try
                {
                    var settings = new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All,
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    };

                    string json = File.ReadAllText(openDialog.FileName);
                    shapes = JsonConvert.DeserializeObject<List<ShapeBase>>(json, settings) ?? new List<ShapeBase>();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке файла: " + ex.Message);
                }
            }
        }

    }
}
