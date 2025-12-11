namespace ProyectofinalLadoCliente.Models.ViewModels
{

    public class ConversionDTO
    {
        public string NumeroOriginal { get; set; } = null!;
        public int BaseOrigen { get; set; }                   
        public int BaseDestino { get; set; }             
    }


    public class PasoConversionDTO
    {
        public int PasoNumero { get; set; }                 
        public string Descripcion { get; set; } = null!;     
        public string ValorIntermedio { get; set; } = null!;  
    }


    public class SimulacionConversionDTO
    {
        public string NumeroOriginal { get; set; } = null!;
        public int BaseOrigen { get; set; }
        public int BaseDestino { get; set; }

        public List<PasoConversionDTO> Pasos { get; set; } = new();

        public string ResultadoFinal { get; set; } = null!;   
    }


    public class EstadisticasConversionDTO
    {
        public string NumeroOriginal { get; set; } = null!;
        public int BaseOrigen { get; set; }
        public int BaseDestino { get; set; }

        public int TotalPasos { get; set; }                   
        public double TiempoMs { get; set; }                  
        public int OperacionesRealizadas { get; set; }        
    }


    public class ConversionCompletaDTO
    {
        public SimulacionConversionDTO Simulacion { get; set; } = null!;
        public EstadisticasConversionDTO Estadisticas { get; set; } = null!;
    }
}
