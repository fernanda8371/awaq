using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ecoQuest1.Pages
{
    public class InstruccionesModel : PageModel

    {
        public string Titulo { get; set; } = "Controles";
        // Propiedad para el mensaje que se mostrará en la página

        public string Message { get; set; } = "En la esquina inferior derecha se encuentran cuatro flechas con las que podrás moverte en el videojuego:<br/><br/>" +
              "<ul>" +
              "<strong>Mover a la Derecha:</strong> Presiona la flecha derecha para mover al jugador hacia la derecha.<br/><br/>" +
              "<strong>Mover a la Izquierda:</strong> Presiona la flecha izquierda para mover al jugador hacia la izquierda.<br/><br/>" +
              "<strong>Subir:</strong> Utiliza la flecha hacia arriba para hacer que el jugador suba.<br/><br/>" +
              "<strong>Bajar:</strong> Utiliza la flecha hacia abajo para hacer que el jugador baje.<br/><br/>" +
              "</ul>";
        //URL del video que se mostrará en la página

        public string VideoUrl { get; set; } = "https://www.youtube.com/embed/jTJPsP0mpX4?si=feZfhCxVoHs9m_kx"; // Establecer un valor predeterminado aquí si es necesario


        // Método para manejar el post cuando se seleccionan los controles del juego

        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {

            ViewData["ShowNavbar"] = true;
            ViewData["ShowIngresarink"] = false;
            ViewData["ShowExitLink"] = true;
            ViewData["ShowClasificacionLink"] = true;
            ViewData["ShowDashboardLink"] = true;
            ViewData["ShowDashboard-AdminLink"] = false;
            ViewData["ShowAddLink"] = false;
            ViewData["ShowEditLink"] = true;
            ViewData["ShowGuideLink"] = true;

            ViewData["ShowInstructionLink"] = true;


            base.OnPageHandlerExecuting(context);
        }

        public void OnPostControles()
        {
            Titulo = "Controles";
            Message = "En la esquina inferior derecha se encuentran cuatro flechas con las que podrás moverte en el videojuego:<br/><br/>" +
              "<ul>" +
              "<strong>Mover a la Derecha:</strong> Presiona la flecha derecha para mover al jugador hacia la derecha.<br/><br/>" +
              "<strong>Mover a la Izquierda:</strong> Presiona la flecha izquierda para mover al jugador hacia la izquierda.<br/><br/>" +
              "<strong>Subir:</strong> Utiliza la flecha hacia arriba para hacer que el jugador suba.<br/><br/>" +
              "<strong>Bajar:</strong> Utiliza la flecha hacia abajo para hacer que el jugador baje.<br/><br/>" +
              "</ul>";

            VideoUrl = "https://www.youtube.com/embed/jTJPsP0mpX4?si=feZfhCxVoHs9m_kx";
        }
        //Método para manejar el post cuando se selecciona la progresión del juego

        public void OnPostProgreso()
        {
            Titulo = "Progresión y Finalización del Juego:";
            Message = "Para completar una casa, el jugador debe obtener una calificación mínima del 80% en las tareas asignadas. Esto implica " +
                      "encontrar todos los objetos perdidos dentro de la casa y responder correctamente a las preguntas relacionadas con el manual " +
                      "correspondiente al módulo de la casa. Si no se alcanza esta calificación mínima, el jugador deberá reiniciar la búsqueda en esa " +
                      "casa.<br/><br/>";
            VideoUrl = "https://www.youtube.com/embed/88cle_mYF4Q?si=dVZuzNPN1vd5hJbe";
        }

        // Método para manejar el post cuando se selecciona cómo ganar el juego

        public void OnPostGanar()
        {
            Titulo = "";
            Message = "Para acreditar y completar el videojuego, el jugador debe cumplir con los siguientes requisitos:<br/>" +
              "<ul>" +
              "<li><strong>Interacción Completa con los Aldeanos:</strong> El jugador debe hablar con todos los aldeanos (NPCs) presentes en la aldea. Esto incluye tanto al aldeano inicial que proporciona la introducción general como a los aldeanos situados en las entradas de cada casa.</li>" +
              "<li><strong>Completar Todas las Casas:</strong> El jugador debe completar todas las casas (Ética, Seguridad y, para colaboradores del departamento de TEDI, la casa de TEDI), obteniendo una calificación mínima del 80% en cada una. Esto requiere encontrar todos los objetos perdidos dentro de cada casa y responder correctamente a las preguntas relacionadas con el manual correspondiente.</li>" +
              "</ul>";

            VideoUrl = "https://www.youtube.com/embed/hkbKr_aYUYs?si=N4jFk7OtTLclLs5z";
        }
    }
}