using Microsoft.AspNetCore.Components;

namespace Blazora.Components
{
    public partial class Butticon : Game_Component
    {

        [Parameter]
        public string picture_filename { get; set; }

        [Parameter]
        public System.Action action { get; set; }

        [Parameter]
        public int val { get; set; }



    }
}