using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.PowerPoint;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Microsoft.Office.Interop.Word;
using Application = Microsoft.Office.Interop.Word.Application;

namespace Agente_de_Automatización_de_investigación
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public class OpenAIClient
        {
            private static readonly string apiUrl = "https://api.openai.com/v1/chat/completions";
            private static readonly string apiKey = "";

            public static async Task<string> ConsultarGPT4Async(string prompt)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                    // Crear el cuerpo de la solicitud
                    var requestBody = new
                    {
                        model = "gpt-4.1", // Modelo a utilizar
                        messages = new[]
                        {
                    new { role = "system", content = "Eres un asistente de investigación." },
                    new { role = "user", content = prompt }
                },
                        max_tokens = 1000, // Ajusta según tus necesidades
                        temperature = 0.7 // Controla la creatividad de las respuestas
                    };

                    // Serializar el cuerpo de la solicitud a JSON
                    var content = new StringContent(
                        Newtonsoft.Json.JsonConvert.SerializeObject(requestBody),
                        Encoding.UTF8,
                        "application/json"
                    );

                    // Enviar la solicitud POST
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    // Asegurarse de que la respuesta sea exitosa
                    response.EnsureSuccessStatusCode();

                    // Leer el contenido de la respuesta
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Deserializar la respuesta y extraer el texto generado
                    dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject(responseBody);
                    return result.choices[0].message.content;
                }
            }
        }


        private async void btBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string prompt = txtPrompt.Text; // Obtén el texto del prompt ingresado por el usuario
                if (string.IsNullOrWhiteSpace(prompt))
                {
                    MessageBox.Show("Por favor, ingresa un prompt válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Llama al método de la API
                string resultado = await OpenAIClient.ConsultarGPT4Async(prompt);

                // Muestra el resultado en el TextBox txtResultado
                txtResultado.Text = resultado;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btLimpiar_Click(object sender, EventArgs e)
        {
            // Limpiar el contenido de los TextBox
            txtPrompt.Text = string.Empty;
            txtResultado.Text = string.Empty;
        }

        private void btPresentacion_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar si hay contenido en el TextBox de resultado  
                if (string.IsNullOrWhiteSpace(txtResultado.Text))
                {
                    MessageBox.Show("No hay contenido para generar la presentación.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Abrir un cuadro de diálogo para que el usuario elija dónde guardar la presentación  
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Presentación de PowerPoint (*.pptx)|*.pptx";
                    saveFileDialog.Title = "Guardar Presentación";
                    saveFileDialog.FileName = "Presentacion.pptx";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Ruta seleccionada por el usuario  
                        string rutaArchivo = saveFileDialog.FileName;

                        // Generar la presentación  
                        GenerarPresentacion("Resultado de la Investigación", rutaArchivo, txtResultado.Text);

                        MessageBox.Show("La presentación se ha generado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al generar la presentación: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerarPresentacion(string titulo, string rutaArchivo, string contenido)
        {
            try
            {
                // Ruta de la plantilla
                string rutaPlantilla = @"C:\Users\kai85\OneDrive\Documents\Plantilla.potx";
                PowerPoint.Application pptApp = new PowerPoint.Application();

                // Abrir la plantilla como una nueva presentación
                PowerPoint.Presentation presentation = pptApp.Presentations.Open(rutaPlantilla,
                    WithWindow: Microsoft.Office.Core.MsoTriState.msoFalse);

                // Usar el diseño de texto de la plantilla
                PowerPoint.CustomLayout customLayout = presentation.SlideMaster.CustomLayouts[PowerPoint.PpSlideLayout.ppLayoutText];

                // Dividir el contenido en párrafos
                string[] parrafos = contenido.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

                int maxCharsPorDiapositiva = 800;

                bool esPrimera = true;
                foreach (string parrafo in parrafos)
                {

                    int inicio = 0;
                    while (inicio < parrafo.Length)
                    {
                        int longitud = Math.Min(maxCharsPorDiapositiva, parrafo.Length - inicio);
                        string textoDiapositiva = parrafo.Substring(inicio, longitud);

                        PowerPoint.Slide slide = presentation.Slides.AddSlide(presentation.Slides.Count + 1, customLayout);
                        slide.Shapes[1].TextFrame.TextRange.Text = esPrimera ? titulo : "";
                        slide.Shapes[2].TextFrame.TextRange.Text = textoDiapositiva;

                        esPrimera = false;
                        inicio += longitud;
                    }
                }

                // Guardar la presentación en la ruta especificada
                presentation.SaveAs(rutaArchivo);
                presentation.Close();
                pptApp.Quit();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al generar la presentación: {ex.Message}");
            }
        }

        private void btDocumento_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar si hay contenido en el TextBox de resultado
                if (string.IsNullOrWhiteSpace(txtResultado.Text))
                {
                    MessageBox.Show("No hay contenido para generar el documento.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Abrir un cuadro de diálogo para que el usuario elija dónde guardar el documento
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Documento de Word (*.docx)|*.docx";
                    saveFileDialog.Title = "Guardar Documento";
                    saveFileDialog.FileName = "Documento.docx";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Ruta seleccionada por el usuario
                        string rutaArchivo = saveFileDialog.FileName;

                        // Generar el documento
                        GenerarDocumento(txtResultado.Text, rutaArchivo);

                        MessageBox.Show("El documento se ha generado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al generar el documento: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerarDocumento(string text, string rutaArchivo)
        {
            try
            {
                // Crear una nueva aplicación de Word
                Application wordApp = new Application();
                Document doc = wordApp.Documents.Add();

                // Agregar contenido al documento
                Paragraph paragraph = doc.Content.Paragraphs.Add();
                paragraph.Range.Text = text; // Usar el texto proporcionado
                paragraph.Range.InsertParagraphAfter();

                // Guardar el documento en la ruta especificada
                doc.SaveAs2(rutaArchivo);
                doc.Close();
                wordApp.Quit();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al generar el documento: {ex.Message}");
            }
        }
    }
}




   

