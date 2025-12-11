using ProyectofinalLadoCliente.Models.ViewModels;
using System.Diagnostics;

namespace ProyectofinalLadoCliente.Services
{
    public class ConversionService
    {
      
        public ConversionCompletaDTO Convertir(ConversionDTO model)
        {
          
            
            var simulacion = GenerarSimulacion(model);

       
           
            var estadisticas = new EstadisticasConversionDTO
            {
                NumeroOriginal = model.NumeroOriginal,
                BaseOrigen = model.BaseOrigen,
                BaseDestino = model.BaseDestino,
                TotalPasos = simulacion.Pasos.Count,
               
                OperacionesRealizadas = CalcularOperaciones(simulacion)
            };

            return new ConversionCompletaDTO
            {
                Simulacion = simulacion,
                Estadisticas = estadisticas
            };
        }

        
        private SimulacionConversionDTO GenerarSimulacion(ConversionDTO model)
        {
            var pasos = new List<PasoConversionDTO>();
            int numeroBase10 = ConvertirABase10(model.NumeroOriginal, model.BaseOrigen, pasos);

            string resultado = ConvertirDesdeBase10(numeroBase10, model.BaseDestino, pasos);

            return new SimulacionConversionDTO
            {
                NumeroOriginal = model.NumeroOriginal,
                BaseOrigen = model.BaseOrigen,
                BaseDestino = model.BaseDestino,
                Pasos = pasos,
                ResultadoFinal = resultado
            };
        }

       
        private int ConvertirABase10(string numero, int baseOrigen, List<PasoConversionDTO> pasos)
        {
            int acumulador = 0;
            int pasoNum = 1;

            var caracteres = numero.ToUpper().ToCharArray();
            int posicionPotencia = caracteres.Length - 1;

            foreach (var c in caracteres)
            {
                int valor = ConvertirCaracter(c);

                int producto = valor * (int)Math.Pow(baseOrigen, posicionPotencia);

                pasos.Add(new PasoConversionDTO
                {
                    PasoNumero = pasoNum++,
                    Descripcion = $"Convertir '{c}' a valor {valor} y multiplicar por {baseOrigen}^{posicionPotencia}",
                    ValorIntermedio = $"{valor} × {baseOrigen}^{posicionPotencia} = {producto}"
                });

                acumulador += producto;
                posicionPotencia--;
            }

            pasos.Add(new PasoConversionDTO
            {
                PasoNumero = pasoNum++,
                Descripcion = "Suma total en base 10",
                ValorIntermedio = acumulador.ToString()
            });

            return acumulador;
        }

      
        private int ConvertirCaracter(char c)
        {
            if (char.IsDigit(c))
                return c - '0';                

            return 10 + (c - 'A');            
        }

       
        private string ConvertirDesdeBase10(int numero, int baseDestino, List<PasoConversionDTO> pasos)
        {
            if (numero == 0)
                return "0";

            int pasoNum = pasos.Count + 1;
            var restos = new List<string>();

            int copia = numero;

            while (copia > 0)
            {
                int residuo = copia % baseDestino;
                int cociente = copia / baseDestino;

                restos.Add(ConvertirAChar(residuo));

                pasos.Add(new PasoConversionDTO
                {
                    PasoNumero = pasoNum++,
                    Descripcion = $"Dividir {copia} entre {baseDestino}",
                    ValorIntermedio = $"Cociente: {cociente}, Residuo: {residuo}"
                });

                copia = cociente;
            }

            restos.Reverse();

            return string.Join("", restos);
        }

      
        private string ConvertirAChar(int valor)
        {
            if (valor < 10)
                return valor.ToString();

            return ((char)('A' + (valor - 10))).ToString();
        }

     
        private int CalcularOperaciones(SimulacionConversionDTO simulacion)
        {
        
            return simulacion.Pasos.Count;
        }
    }
}
