using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheConnectome
{
    public class ConnectomeThread
    {
        public static void NodeOne(Synapse n, StreamWriter writer)
        {
            //Console.WriteLine("\tNode1, {0} {1}", n.NeuronA, n.NeuronB);
            //writer.WriteLine("\tNode1, {0} {1}", n.NeuronA, n.NeuronB);
            TheConnectome.RunConnectome(n, writer, "NodeOne");
        }

        public static void NodeTwo(Synapse n, StreamWriter writer)
        {
            //Console.WriteLine("\tNode2, {0} {1}", n.NeuronA, n.NeuronB);
            //writer.WriteLine("\tNode2, {0} {1}", n.NeuronA, n.NeuronB);
            TheConnectome.RunConnectome(n, writer, "NodeTwo");
        }

        public static void NodeThree(Synapse n, StreamWriter writer)
        {
            //Console.WriteLine("\tNode3, {0} {1}", n.NeuronA, n.NeuronB);
            //writer.WriteLine("\tNode3, {0} {1}", n.NeuronA, n.NeuronB);
            TheConnectome.RunConnectome(n, writer, "NodeThree");
        }

        public static void NodeFour(Synapse n, StreamWriter writer)
        {
            //Console.WriteLine("\tNode4, {0} {1}", n.NeuronA, n.NeuronB);
            //writer.WriteLine("\tNode4, {0} {1}", n.NeuronA, n.NeuronB);
            TheConnectome.RunConnectome(n, writer, "NodeFour");
        }

        public static void NodeFive(Synapse n, StreamWriter writer)
        {
            //Console.WriteLine("\tNode5, {0} {1}", n.NeuronA, n.NeuronB);
            //writer.WriteLine("\tNode5, {0} {1}", n.NeuronA, n.NeuronB);
            TheConnectome.RunConnectome(n, writer, "NodeFive");
        }
    }
}