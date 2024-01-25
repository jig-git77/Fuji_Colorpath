#region System
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace Taskero.Business
{
    public abstract class PressPatchCalculations
    {
        #region PatchCalculationsID
        int _PatchCalculationsID = 0;
        public int PatchCalculationsID { get { return _PatchCalculationsID; } set { _PatchCalculationsID = value; } }
        #endregion

        #region SheetID
        string _DongleID = "";
        public string DongleID { get { return _DongleID; } set { _DongleID = value; } }
        #endregion

        #region JobID
        int _JobID = 0;
        public int JobID { get { return _JobID; } set { _JobID = value; } }
        #endregion

        #region SheetID
        int _SheetID = 0;
        public int SheetID { get { return _SheetID; } set { _SheetID = value; } }
        #endregion

        #region ColorID
        int _ColorID = 0;
        public int ColorID { get { return _ColorID; } set { _ColorID = value; } }
        #endregion
        
        #region ZoneNumber
        int _ZoneNumber = 0;
        public int ZoneNumber { get { return _ZoneNumber; } set { _ZoneNumber = value; } }
        #endregion

        #region Density
        decimal _Density = 0;
        public decimal Density { get { return _Density; } set { _Density = value; } }
        #endregion

        #region CIELAB_L
        decimal _CIELAB_L = 0;
        public decimal CIELAB_L { get { return _CIELAB_L; } set { _CIELAB_L = value; } }
        #endregion

        #region CIELAB_A
        decimal _CIELAB_A = 0;
        public decimal CIELAB_A { get { return _CIELAB_A; } set { _CIELAB_A = value; } }
        #endregion

        #region CIELAB_B
        decimal _CIELAB_B = 0;
        public decimal CIELAB_B { get { return _CIELAB_B; } set { _CIELAB_B = value; } }
        #endregion

        #region deltaE
        decimal _deltaE = 0;
        public decimal deltaE { get { return _deltaE; } set { _deltaE = value; } }
        #endregion

        #region deltaE94
        decimal _deltaE94 = 0;
        public decimal deltaE94 { get { return _deltaE94; } set { _deltaE94 = value; } }
        #endregion

        #region deltaE2000
        decimal _deltaE2000 = 0;
        public decimal deltaE2000 { get { return _deltaE2000; } set { _deltaE2000 = value; } }
        #endregion

        #region deltaECMC21
        decimal _deltaECMC21 = 0;
        public decimal deltaECMC21 { get { return _deltaECMC21; } set { _deltaECMC21 = value; } }
        #endregion

        #region deltaECMC11
        decimal _deltaECMC11 = 0;
        public decimal deltaECMC11 { get { return _deltaECMC11; } set { _deltaECMC11 = value; } }
        #endregion

        #region DotGain
        decimal _DotGain = 0;
        public decimal DotGain { get { return _DotGain; } set { _DotGain = value; } }
        #endregion

        #region PrintContrast
        decimal _PrintContrast = 0;
        public decimal PrintContrast { get { return _PrintContrast; } set { _PrintContrast = value; } }
        #endregion

        #region Trap
        decimal _Trap = 0;
        public decimal Trap { get { return _Trap; } set { _Trap = value; } }
        #endregion

        #region Brightness
        decimal _Brightness = 0;
        public decimal Brightness { get { return _Brightness; } set { _Brightness = value; } }
        #endregion

        #region Hue
        decimal _Hue = 0;
        public decimal Hue { get { return _Hue; } set { _Hue = value; } }
        #endregion

        #region Chroma
        decimal _Chroma = 0;
        public decimal Chroma { get { return _Chroma; } set { _Chroma = value; } }
        #endregion

        #region G7NeutralDensity
        decimal _G7NeutralDensity = 0;
        public decimal G7NeutralDensity { get { return _G7NeutralDensity; } set { _G7NeutralDensity = value; } }
        #endregion

        #region G7DeltaEAB
        decimal _G7DeltaEAB = 0;
        public decimal G7DeltaEAB { get { return _G7DeltaEAB; } set { _G7DeltaEAB = value; } }
        #endregion

        #region PatchStatusTypeId
        Int16 _PatchStatusTypeId = 0;
        public Int16 PatchStatusTypeId { get { return _PatchStatusTypeId; } set { _PatchStatusTypeId = value; } }
        #endregion        
    }
}
