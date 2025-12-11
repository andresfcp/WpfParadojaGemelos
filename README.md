# WpfParadojaGemelos

Aplicación WPF que ilustra la paradoja de los gemelos mediante fórmulas relativistas: dilatación temporal y masa relativista. Interfaz sencilla para introducir parámetros, calcular resultados, almacenar series de datos y generar gráficos.

## Resumen rápido

La aplicación calcula:

- Tiempo observado por un marco inercial (observador) a partir del tiempo propio del viajero y la velocidad (porcentaje de c).
- Masa relativista a partir de la masa en reposo y la velocidad.

Fácil de usar, registra resultados en un `ObservableCollection<Dato>` y permite graficar la evolución frente al porcentaje de la velocidad de la luz.

## Fórmulas principales

Tiempo del observador (to) en función del tiempo del viajero (tv) y la velocidad V:

$$
to=\left(\frac{tv}{\sqrt{\frac{1-V^2}{C^2}}}\right)
$$

En la UI la velocidad de la luz en el vacío se introduce como porcentaje de `C` (por ejemplo, `porcentaje = 90` significa V = 0.9·C). La aplicación controla el caso límite de 100% de la velocidad de la luz y evita divisiones por cero mostrando "∞".

## Características principales

- Cálculo de dilatación temporal y masa relativista.
- Almacenamiento en `ObservableCollection<Dato>` y visualización en `DataGrid`.
- Modo de graficado que genera una serie de valores para distintos porcentajes de `c`.
- Operaciones: limpiar datos, eliminar registros individuales y ventana "About".
- Animación del cohete con `Storyboard` y `TranslateTransform`.
- Validación básica de entradas (masa).

## Requisitos

- .NET 9
- Visual Studio 2026 con workload de desarrollo de escritorio (.NET Desktop Development)
- Windows con soporte para WPF

## Compilar y ejecutar

1. Clonar el repositorio:
``` bash
git clone https://github.com/andresfcp/WpfParadojaGemelos.git cd WpfParadojaGemelos
```

2. Abrir la solución en Visual Studio 2026: __File > Open > Project/Solution__ y seleccionar la solución.

3. Restaurar paquetes si procede: usar __Tools > NuGet Package Manager > Package Manager Console__ y ejecutar `dotnet restore`, o usar la UI de NuGet.

4. Establecer `WpfParadojaGemelos` como proyecto de inicio y ejecutar con __Debug > Start Debugging__ o __Ctrl+F5__.

## Uso

- Introducir `Tiempo viajero` y `Masa en reposo`.
- Ajustar el deslizador de `Porcentaje de c` y pulsar `Calcular`.
- Marcar `Graficar` para generar una serie completa y habilitar el botón de gráfico.
- `Limpiar` borra la tabla; `Eliminar` quita el registro seleccionado.

Notas:
- El caso límite de 100% de la velocidad de la luz se muestra como infinito (∞).
- La entrada de masa se valida y muestra advertencia si no es numérica.

## Estructura del proyecto (resumen)

- `MainWindow.xaml` / `MainWindow.xaml.cs`: UI principal y lógica de eventos.
- `Models/Dato.cs`: modelo de dato usado en el `DataGrid`.
- `Views/Grafico.xaml` / `Grafico.xaml.cs`: ventana para graficar la colección.
- `Views/About.xaml` / `About.xaml.cs`: información de la aplicación.
- `Themes/*.xaml`: recursos (colores, tipografía, estilos).

## Buenas prácticas

- Seguir las reglas definidas en `.editorconfig` (sangrado, nombres, estilo).
- Mantener la lógica de UI en code-behind mínima; valorar migración a MVVM si el proyecto crece.
- Commits claros y atómicos (ej.: `feat: add chart`, `fix: validate mass input`).

## Contribuir

1. Fork y crear rama descriptiva: `feature/nombre` o `fix/descripcion`.
2. Verificar que el código compila y respeta `.editorconfig`.
3. Commits pequeños y descriptivos.
4. Abrir Pull Request describiendo los cambios.

## Depuración y diagnóstico

- Revisar la ventana Output en Visual Studio: __View > Output__.
- Para problemas de UI/animación usar: __Debug > Windows > Live Visual Tree__ y __Live Property Explorer__.
- Ejecutar en modo Debug para capturar excepciones y puntos de interrupción.

## Licencia y autor

Repositorio original: https://github.com/andresfcp/WpfParadojaGemelos  
Si procede, añade un `LICENSE` (por ejemplo, MIT) en la raíz del repositorio.

## Contacto

Abrir un _issue_ o enviar un Pull Request en el repositorio para sugerencias o correcciones.
