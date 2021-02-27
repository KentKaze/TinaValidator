﻿using System.Collections.Generic;
using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Aritiafel.Artifacts.TinaValidator.Serialization;
using Aritiafel.Artifacts.Calculator;
using Aritiafel.Locations.StorageHouse;
using System.IO;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class ValidateLogic : Area
    {
        public List<Area> Areas { get; set; } = new List<Area>();
        public Dictionary<string, TNode> TNodes { get; set; } = new Dictionary<string, TNode>();
        //public Dictionary<string, Variable> Variables { get; set; } = new Dictionary<string, Variable>();
        private void Save(TNode node, Area a)
        {
            if (node == null)
                throw new Exception($"Node Missing");

            if (TNodes.ContainsKey(node.ID))
                return;
            TNodes.Add(node.ID, node);

            switch(node)
            {
                case EndNode _:
                    return;
                case AreaStart _:
                    Save(node.NextNode, a);
                    break;
                case Status st:
                    for (int i = 0; i < st.Choices.Count; i++)
                        Save(st.Choices[i].Node, a);
                    break;
                case null:
                    throw new ArgumentNullException(nameof(node));
                default:
                    Save(node.NextNode, a);
                    break;
            }
        }

        public void Load(string filePath)
        {   
            string jsonString;
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    jsonString = sr.ReadToEnd();
                }
            }

            JsonSerializerOptions jso = new JsonSerializerOptions
            { WriteIndented = true };
            jso.Converters.Add(new TNodeJsonConverter());
            jso.Converters.Add(new AreaJsonConverter());
            jso.Converters.Add(new ChoiceJsonConverter());
            jso.Converters.Add(new StatementJsonConverter());
            jso.Converters.Add(new UnitConverter());
            jso.Converters.Add(new OtherJsonConverter());
            ValidateLogic vl = JsonSerializer.Deserialize<ValidateLogic>(jsonString, jso);
            this.Areas = vl.Areas;
            this.Name = vl.Name;
            this.Parent = vl.Parent;
            this.InitialStatus = vl.InitialStatus;
        }

        public void Save(string filePath)
        {
            TNodes.Clear();
            for (int i = 0; i < Areas.Count; i++)            
                Save(Areas[i].InitialStatus, Areas[i]);
            Save(InitialStatus, this);
            JsonSerializerOptions jso = new JsonSerializerOptions
            { WriteIndented = true };

            jso.Converters.Add(new TNodeJsonConverter());
            jso.Converters.Add(new AreaJsonConverter());
            jso.Converters.Add(new ChoiceJsonConverter());
            jso.Converters.Add(new StatementJsonConverter());
            jso.Converters.Add(new UnitConverter());
            jso.Converters.Add(new OtherJsonConverter());
            string s = JsonSerializer.Serialize(this, jso);
            using(FileStream fs = new FileStream(filePath, FileMode.Create))                
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(s);
                }
            }
        }

        public ValidateLogic()
            : this(null)
        { }
        public ValidateLogic(Status initialStatus = null)
            : this(null, initialStatus)
        { }

        public ValidateLogic(string name, Status initialStatus = null)
            : base(name, initialStatus, null)
        { }
    }
}
