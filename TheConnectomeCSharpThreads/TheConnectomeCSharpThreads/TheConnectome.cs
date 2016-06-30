using System;
using System.Collections.Generic;
using System.IO;

namespace TheConnectome
{
    public class TheConnectome
    {
        // Global Lists
        public static readonly List<Synapse> Connectome = new List<Synapse>();

        public static readonly List<PostSynapticNeuron> Postsynaptic = new List<PostSynapticNeuron>();

        // CSV Files

        // windows file strings (VS Debug Folder)
        //public static readonly string ConnectomeFile = "connectome.csv";

        //public static readonly string PostsynapticFile = "postsynaptic.csv";

        public static readonly string ConnectomeFile = "edgelist.csv";

        public static readonly string PostsynapticFile = "synaptic.csv";

        // Global Variables
        public static int Threshold = 15;

        public static int MuscleFireCount;
        public static int NeuronFireCount;

        /*****  METHODS  *****/

        /*
     *  TestFilesWereRead()
     *  display contents of the Lists Connectome, Postsynaptic
     *  if data displays then the files were read correctly
     *
     */

        public static void TestFilesWereRead(StreamWriter writer)
        {
            writer.WriteLine("Displaying Contents of Connectome and Postsynaptic Lists:\n\n");

            int i;

            if (Connectome.Count > 0)
            {
                //Console.WriteLine("Connectome List size: " + Connectome.Count);
                writer.WriteLine("Connectome List size: " + Connectome.Count);

                for (i = 0; i < Connectome.Count; i++)
                {
                    //Console.WriteLine("\t" + i + " " + Connectome[i].NeuronA);
                    //Console.WriteLine("\t" + i + " " + Connectome[i].NeuronB);
                    //Console.WriteLine("\t" + i + " " + Connectome[i].Weight);

                    writer.WriteLine("\t" + i + " " + Connectome[i].NeuronA);
                    writer.WriteLine("\t" + i + " " + Connectome[i].NeuronB);
                    writer.WriteLine("\t" + i + " " + Connectome[i].Weight);
                }
            }
            else
            {
                Console.WriteLine("Connectome List has no data.");
                writer.WriteLine("Connectome List has no data.");
            }

            if (Postsynaptic.Count > 0)
            {
                Console.WriteLine("Postsynaptic List size: " + Postsynaptic.Count);

                for (i = 0; i < Postsynaptic.Count; i++)
                {
                    //Console.WriteLine("\t" + i + " " + Postsynaptic[i].NeuronA);
                    //Console.WriteLine("\t" + i + " " + Postsynaptic[i].Weight);

                    writer.WriteLine("\t" + i + " " + Postsynaptic[i].NeuronA);
                    writer.WriteLine("\t" + i + " " + Postsynaptic[i].Weight);
                }
            }
            else
            {
                //Console.WriteLine("Postsynaptic List has no data.");
                writer.WriteLine("Postsynaptic List has no data.");
            }

            writer.WriteLine("\n\n");
        }

        // end TestFilesWereRead()

        /*
     *  ReadConnectomeFile()
     *  read the .csv file being read into the Connectome List
     *  synapse(neuronA,neuronB,weight)
     *
     */

        public static void ReadConnectomeFile(StreamWriter writer)
        {
            //int counter = 0;
            using (var reader = new StreamReader(File.OpenRead(ConnectomeFile)))
            {
                try
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();

                        if (!string.IsNullOrEmpty(line))
                        {
                            var values = line.Split(',');
                            Connectome.Add(new Synapse(values[0], values[1], int.Parse(values[2])));
                        }
                        else
                        {
                            Console.WriteLine("line was null");
                        }
                    }
                    // end while
                }
                catch (Exception e)
                {
                    Console.WriteLine("File [" + ConnectomeFile + "] could not be read.");
                    Console.WriteLine("ERROR: " + e.Message);
                }
                finally
                {
                    Console.WriteLine("File {0} successfully read", ConnectomeFile.ToString());
                    writer.WriteLine("File {0} successfully read", ConnectomeFile.ToString());
                }
                // end try/catch

                reader.Close();
            }
            // end using
        }

        // end ReadConnectomeFile()

        /*
     *  ReadPostsynapticFile()
     *  read the .csv file being read into the Postsynaptic List
     *  Synapse(neuronA,weight)
     *
     */

        public static void ReadPostsynapticFile(StreamWriter writer)
        {
            using (var reader = new StreamReader(File.OpenRead(PostsynapticFile)))
            {
                try
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();

                        if (!string.IsNullOrEmpty(line))
                        {
                            var values = line.Split(',');
                            Postsynaptic.Add(new PostSynapticNeuron(values[0], 0));
                        }
                        else
                        {
                            Console.WriteLine("line was null");
                        }
                    }
                    // end while
                }
                catch (Exception e)
                {
                    Console.WriteLine("File [" + PostsynapticFile + "] could not be read.");
                    Console.WriteLine("ERROR: " + e.Message);
                }
                finally
                {
                    Console.WriteLine("File {0} successfully read", PostsynapticFile.ToString());
                    writer.WriteLine("File {0} successfully read", PostsynapticFile.ToString());
                }

                // end try/catch

                reader.Close();
            }
            // end using
        }

        // end ReadPostsynapticFile()

        /*
     *  DendriteAccumulate(Synapse)
     *
     *
     */

        public static void DendriteAccumulate(Synapse synapse, StreamWriter writer, string threadName)
        {
            foreach (var con in Connectome)
            {
                if (con.NeuronA == synapse.NeuronA)
                {
                    foreach (var postsyn in Postsynaptic)
                    {
                        if (postsyn.NeuronA == con.NeuronB)
                        {
                            postsyn.Weight = con.Weight;

                            // output for file to show Postsynaptic Weight changes
                            /*
                            Console.WriteLine("\t[{0}]Postsynaptic Weight: {1} {2} + {3} =  {4}", threadName,
                                postsyn.NeuronA, postsyn.Weight - con.Weight, con.Weight, postsyn.Weight);
                            writer.WriteLine("\t[{0}]Postsynaptic Weight: {1} {2} + {3} =  {4}", threadName,
                                postsyn.NeuronA, postsyn.Weight - con.Weight, con.Weight, postsyn.Weight);
                            */
                        }
                    }
                }
            }
        }

        // end DendriteAccumulate(Synapse)

        /*
     *  FireNeuron(Synapse)
     *
     *
     */

        public static void FireNeuron(Synapse neuron, StreamWriter writer, string threadName)
        {
            //Console.WriteLine("DendriteAccumulate [sent from FireNeuron] : {0} {1} {2}", neuron.NeuronA, neuron.NeuronB, neuron.Weight.ToString());
            //writer.WriteLine("DendriteAccumulate [sent from FireNeuron] : {0} {1} {2}", neuron.NeuronA, neuron.NeuronB, neuron.Weight.ToString());
            DendriteAccumulate(neuron, writer, threadName);
            foreach (var postsyn in Postsynaptic)
            {
                if (Math.Abs(postsyn.Weight) > Threshold)
                {
                    if (postsyn.NeuronA == "PLMR" ||
                        postsyn.NeuronA == "PLML" ||
                        postsyn.NeuronA.Substring(0, 2) == "MV" ||
                        postsyn.NeuronA.Substring(0, 2) == "MD")
                    {
                        MuscleFireCount++;
                        Console.WriteLine("[{0}] Fire Muscle {1}{2}", threadName, postsyn.NeuronA, postsyn.NeuronB);
                        writer.WriteLine("[{0}] Fire Muscle {1}{2}", threadName, postsyn.NeuronA, postsyn.NeuronB);
                        postsyn.ResetWeight = 0;
                    }
                    else
                    {
                        NeuronFireCount++;
                        Console.WriteLine("[{0}] Fire Neuron {1}", threadName, postsyn.NeuronA);
                        writer.WriteLine("[{0}] Fire Neuron {1}", threadName, postsyn.NeuronA);
                        DendriteAccumulate(postsyn, writer, threadName);
                        postsyn.ResetWeight = 0;
                    }
                }
            }
        }

        // end FireNeuron(Synapse)

        /*
     *  RunConnectome(Synapse)
     *
     *
     */

        public static void RunConnectome(Synapse neuron, StreamWriter writer, string threadName)
        {
            //Console.WriteLine("DendriteAccumulate [sent from RunConnectome] : {0} {1} {2}", neuron.NeuronA, neuron.NeuronB, neuron.Weight.ToString());
            //writer.WriteLine("DendriteAccumulate [sent from RunConnectome] : {0} {1} {2}", neuron.NeuronA, neuron.NeuronB, neuron.Weight.ToString());

            DendriteAccumulate(neuron, writer, threadName);

            foreach (var postsyn in Postsynaptic)
            {
                //Console.WriteLine("[RunConnectome] postsyn: {0} {1} ", postsyn.NeuronA, postsyn.Weight.ToString());
                //writer.WriteLine("[RunConnectome] postsyn: {0} {1} ", postsyn.NeuronA, postsyn.Weight.ToString());

                if (Math.Abs(postsyn.Weight) > Threshold)
                {
                    //Console.WriteLine("{0} > {1}", Math.Abs(postsyn.Weight).ToString(), Threshold);
                    FireNeuron(postsyn, writer, threadName);
                    postsyn.ResetWeight = 0;
                }
            }
        }

        // end RunConnectome(Synapse)
    }

    // end class
}

// end