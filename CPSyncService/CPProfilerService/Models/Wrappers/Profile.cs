using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

[DataContract]
public class Profile
{
        
        private byte[] _ICCProfile;
        [DataMember()]
        public byte[] ICCProfile {
	          get { return this._ICCProfile; }
	          set { this._ICCProfile = value; }
        }

        private double[] _Cyan = new double[101] ;
        [DataMember()]
        public double[] Cyan {
	          get { return this._Cyan; }
	          set { this._Cyan = value; }
        }

        private double[] _Magenta = new double[101]  ;
        [DataMember()]
        public double[] Magenta {
	        get { return this._Magenta; }
	        set { this._Magenta = value; }
        }

        private double[] _Yellow = new double[101] ;
        [DataMember()]
        public double[] Yellow {
	        get { return this._Yellow; }
	        set { this._Yellow = value; }
        }
        
        private double[] _Key = new double[101] ;
        [DataMember()]
        public double[] Key {
	        get { return this._Key; }
	        set { this._Key = value; }
        }


        private string _ErrorMsg;
        [DataMember()]
        public string ErrorMsg {
            get { return this._ErrorMsg; }
            set { this._ErrorMsg = value; }

        }
}
 
