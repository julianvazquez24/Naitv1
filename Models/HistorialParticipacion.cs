namespace Naitv1.Models

{
    public class HistorialParticipacion
    {
        public int IdUsuario { get; set; }
        public Usuario? Usuario { get; set; }
        public int IdActividad { get; set; }
        public Actividad? Actividad { get; set; }
}
}
