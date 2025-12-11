using System;
using System.Diagnostics;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        // 1. CONFIGURACIÓN DEL ESCENARIO
        // Generamos una cantidad masiva de datos para que la diferencia sea notable.
        int cantidadDatos = 10000000; // 10 Millones de datos
        Console.WriteLine($"--- GENERANDO {cantidadDatos:N0} DATOS ALEATORIOS ---");

        int[] datos = new int[cantidadDatos];
        Random rnd = new Random();

        // Llenamos el arreglo
        for (int i = 0; i < cantidadDatos; i++)
        {
            datos[i] = rnd.Next(0, cantidadDatos * 2); // Números entre 0 y 20 millones
        }

        // Elegimos un número que SABEMOS que existe (el último para el peor caso lineal)
        // Ojo: En un escenario real, el número podría estar en cualquier parte.
        int objetivo = datos[cantidadDatos - 1];
        Console.WriteLine($"Objetivo a buscar: {objetivo}");
        Console.WriteLine("--------------------------------------------------\n");

        // 2. PRUEBA DE BÚSQUEDA LINEAL
        Console.WriteLine("INICIANDO BÚSQUEDA LINEAL...");
        Stopwatch sw = Stopwatch.StartNew();

        int indiceLineal = BusquedaLineal(datos, objetivo);

        sw.Stop();
        Console.WriteLine($"[Lineal] Encontrado en índice: {indiceLineal}");
        Console.WriteLine($"[Lineal] Tiempo transcurrido: {sw.Elapsed.TotalMilliseconds} ms");
        Console.WriteLine("Justificación: Aceptable para pocos datos, pero lento aquí.");
        Console.WriteLine("\n--------------------------------------------------\n");

        // 3. PRUEBA DE BÚSQUEDA BINARIA
        // Para usar búsqueda binaria, PRIMERO debemos ordenar.
        Console.WriteLine("PREPARANDO BÚSQUEDA BINARIA (ORDENANDO DATOS)...");
        sw.Restart();

        Array.Sort(datos); // El costo de ordenar

        sw.Stop();
        Console.WriteLine($"[Ordenamiento] Tiempo de preparación: {sw.Elapsed.TotalMilliseconds} ms");

        Console.WriteLine("INICIANDO BÚSQUEDA BINARIA...");
        sw.Restart();

        int indiceBinario = BusquedaBinaria(datos, objetivo);

        sw.Stop();
        Console.WriteLine($"[Binaria] Encontrado en índice (post-orden): {indiceBinario}");
        Console.WriteLine($"[Binaria] Tiempo transcurrido: {sw.Elapsed.TotalMilliseconds} ms"); // Esto será casi 0
        Console.WriteLine("Justificación: Una vez ordenado, la búsqueda es instantánea.");

        Console.ReadKey();
    }

    // Algoritmo de Búsqueda Lineal O(n)
    static int BusquedaLineal(int[] arreglo, int valor)
    {
        for (int i = 0; i < arreglo.Length; i++)
        {
            if (arreglo[i] == valor) return i;
        }
        return -1;
    }

    // Algoritmo de Búsqueda Binaria O(log n)
    static int BusquedaBinaria(int[] arreglo, int valor)
    {
        int izquierdo = 0;
        int derecho = arreglo.Length - 1;

        while (izquierdo <= derecho)
        {
            int medio = izquierdo + (derecho - izquierdo) / 2;

            if (arreglo[medio] == valor)
                return medio;

            if (arreglo[medio] < valor)
                izquierdo = medio + 1;
            else
                derecho = medio - 1;
        }
        return -1;
    }
}