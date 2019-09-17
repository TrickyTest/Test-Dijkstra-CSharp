using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrickyUnits;
using TrickyUnits.Dijkstra;

namespace TestDijkstra {
    class TestDijkstra {
        static void Main(string[] args) {
            // Diagonally?
            Dijkstra.Config_AllowDiagonally = false;
            // Create BlockMap
            var map = QuickStream.LoadLines($"{qstr.ExtractDir(MKL.MyExe)}/TestDijkStra_Map.txt");
            var buf = new bool[map[0].Length, map.Length];
            int SX = 0, SY = 0, EX = 0, EY = 0;
            for (int y = 0; y < map.Length; y++) for (int x = 0; x < map[y].Length; x++) {
                    buf[x, y] = map[y][x] != 'X';
                    switch (map[y][x]) {
                        case 'E':
                            EX = x;
                            EY = y;
                            Console.WriteLine($"  End position: ({EX},{EY})");
                            break;
                        case 'S':
                            SX = x;
                            SY = y;
                            Console.WriteLine($"Start position: ({SX},{SY})");
                            break;
                    }
                }
            Console.WriteLine($"Calculating route from ({SX},{SY}) to ({EX},{EY})");
            var D = new Dijkstra(buf, SX, SY, EX, EY);
            var Route = D.Start();
            Console.WriteLine($"Success: {Route.Success} - Length: {Route.Nodes.Length}");
            int u = 0; // unit count!
            for (int y = 0; y < map.Length; y++) {
                if (y == 0) {
                    var t = "";
                    var e = "";
                    for (int i = 0; i < map[y].Length; i++) {
                        if (i % 10 == 0) t += $"{(int)Math.Floor((decimal)i / 10)}"; else t += " ";
                        e += i % 10;
                    }
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"{t}\n{e}");
                }
                for (int x = 0; x < map[y].Length; x++) {
                    //int m = u % 10;
                    var hasnode = false;
                    foreach (Node N in Route.Nodes) hasnode = hasnode || (N.x == x && N.y == y);
                    if (hasnode) {
                        Console.ForegroundColor = (ConsoleColor)9;
                        Console.Write('*');
                    } else if (!(buf[x, y])) {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("X");
                    } else {
                        Console.Write(" ");
                    }

                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"    {y}");
            }
            Console.ResetColor();
            foreach (Node N in Route.Nodes) Console.Write($"({N.x},{N.y}); ");
            Console.WriteLine("\n"); // Yes, skip two lines... I know dirty code!
            TrickyDebug.AttachWait();
        }
    }
}
