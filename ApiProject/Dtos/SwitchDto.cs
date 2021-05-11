namespace ApiProject.Dtos
{
    public class SwitchDto
    {
        public int Id { get; set; }

        public string Manufacturer { get; set; }
        public string FullName { get; set; }
        public string SwitchType { get; set; }

        public int ActuationForce { get; set; }
        public int BottomOutForce { get; set; }

        public float ActuationDistance { get; set; }
        public float BottomOutDistance { get; set; }

        public int Lifespan { get; set; }
    }
}