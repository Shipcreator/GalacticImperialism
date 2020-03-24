using System;
using System.Collections.Generic;
using System.Linq;
using Lidgren.Network;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace GalacticImperialism.Networking
{
    class ConnectionHandler
    {

        NetPeer peer;
        Game1 game;

        public ConnectionHandler(Random rand, Game1 g)
        {
            // Creates a unique identifier, thats what the string is for (used for people on the same ports)
            NetPeerConfiguration config = new NetPeerConfiguration("GalaticImperialism");
            config.PingInterval = 6; // Not entirely sure oops.
            config.MaximumConnections = 4; // Maximum amount of connections you're allowed to have, set this to 4 since max is 4 players.
            config.AcceptIncomingConnections = true; // Allows people to connect to you essentially
            config.AutoFlushSendQueue = true; // Not entirely sure.
            config.EnableMessageType(NetIncomingMessageType.DiscoveryResponse); // Essentially these two lines say what type of messages are allowed
            config.EnableMessageType(NetIncomingMessageType.DiscoveryRequest); // and what we will handle and receive essentially.
            peer = new NetPeer(config); // Creates the NetPeer object with the config above.

            game = g;
        }

        public void Start()
        {
            peer.Start(); // Actually starts up the connection to be used from the above config.
        }

        public void End()
        {
            peer.Shutdown("Shutting down"); // Should in theory turn off the connection.
        }

        public void FindPeer(string port)
        {
            if (port != null && port.Length > 0)
                peer.DiscoverLocalPeers(int.Parse(port)); // Sends out a broadcast on your local subnet looking for that port number with a message type
            // of DiscoveryRequest, and if someone receives it they will send back a DiscoveryResponse which then we will connect to them.
        }

        public NetPeer getCon()
        {
            return peer; // Returns the NetPeer connection.
        }

        public byte[] SerializeData(Object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public Object DeserializeData(byte[] bytes)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream(bytes))
            {
                return bf.Deserialize(ms);
            }
        }

        public void Listener() // Listens for incoming messages on your peer port.
        {
            NetIncomingMessage message = peer.ReadMessage(); // Reads any incoming messages assuming there are any.
            if (message != null) // Makes sure there actually is a message.
            {
                switch (message.MessageType) // Checks for the message type that we grabbed above.
                {
                    case NetIncomingMessageType.DiscoveryRequest: // Someone is looking for you because they requested this port number.

                        NetOutgoingMessage res = peer.CreateMessage(); // Creates a response / packet to send back.
                        res.Write("Here!"); // Replies with "Here!" in theory you could customize this for different networks to choose what to do
                        peer.SendDiscoveryResponse(res, message.SenderEndPoint);
                        break;
                    case NetIncomingMessageType.DiscoveryResponse: // Your request was received and you got a response back.
                        peer.Connect(message.SenderEndPoint);  // Since you got a message back you connect to the end point.
                        break;
                    case NetIncomingMessageType.Data: // You received Data or an update from a peer
                        Console.WriteLine(DeserializeData(message.ReadBytes(message.LengthBytes)));
                        //game.counter = message.ReadInt32();  // Grabs data.
                        //game.data = message.ToString(); // Basically turns your message into a string as said.
                        break;
                }
            }
        }
    }
}
