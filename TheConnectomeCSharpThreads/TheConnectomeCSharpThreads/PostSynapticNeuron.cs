namespace TheConnectome
{
    public class PostSynapticNeuron : Synapse
    {
        public PostSynapticNeuron()
        {
            NeuronA = "";
            Weight = 0;
        }

        public PostSynapticNeuron(string a, int w)
        {
            NeuronA = a;
            Weight = w;
        }

        public int ResetWeight
        {
            get { return 0; }
            set { weight = value; }
        }
    }
}