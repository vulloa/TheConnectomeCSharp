﻿namespace TheConnectome
{
    public class Synapse
    {
        // variables

        public int weight;

        // constructors
        public Synapse()
        {
            NeuronA = "";
            NeuronB = "";
            weight = 0;
        }

        //public Synapse(string a, int c)
        //{
        //    NeuronA = a;
        //    _weight = c;
        //}

        public Synapse(string a, string b, int c)
        {
            NeuronA = a;
            NeuronB = b;
            Weight = c;
        }

        // methods

        public string NeuronA { get; set; }

        public string NeuronB { get; set; }

        public int Weight
        {
            get { return weight; }
            set { weight += value; }
        }

        //}
        //    set { weight = value; }
        //    get { return 0; }
        //{
        //public int ResetWeight
    }

    // end class
}

// end namespace