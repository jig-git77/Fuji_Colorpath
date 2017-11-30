//Object for Current Run Status
var EOptimizeRunStatus = {
    Open: 1,
    Closed: 2,
    Certify: 3
}

//Object for E License Type
var ELicenseType = {
    None: 0,
    Sync1D: 1,
    DLP: 2,
    InkOptimization: 3,
    InkOptimizationSource: 4,
    Sync4DCertification: 5,
    CustomSourceProfile: 6,
    ReProfileCustomSource: 7,
    FullGamutCustomSource: 8,
    MultiColorCustomSource: 9,
    SpotLibrary: 10,
    Validation: 11,
    IccProfiling: 12
}

//Object for E Validation Status
var EValidationStatus = {
    None: 0,
    NotCertified: 1,
    Disabled: 2,
    NoMeasurement: 3,
    LastOK: 4,
    LastFailed: 5
}


var CPPatchType = {
    Cyan: 1,
    Magenta: 2,
    Yellow: 3,
    Black: 4,
    Overprint_Red: 5,
    Overprint_Green: 6,
    Overprint_Blue: 7,
    Paper: 8,
    Triplet_Solid_Gray: 9,
    Gray_C_M_Y: 10,
    Other: 11,
    Cyan_Tint: 12,
    Magenta_Tint: 13,
    Yellow_Tint: 14,
    Black_Tint: 15,
    Cyan_Quarter_Tone: 16,
    Cyan_Half_Tone: 17,
    Cyan_Three_Quarter_Tone: 18,
    Magenta_Quarter_Tone: 19,
    Magenta_Half_Tone: 20,
    Magenta_Three_Quarter_Tone: 21,
    Yellow_Quarter_Tone: 22,
    Yellow_Half_Tone: 23,
    Yellow_Three_Quarter_Tone: 24,
    Black_Quarter_Tone: 25,
    Black_Half_Tone: 26,
    Black_Three_Quarter_Tone: 27,
    Gray_25: 28,
    Gray_50: 29,
    Gray_75: 30,

    //Newly added PatchTypes into CP.PatchType_Sync1D table
    Cyan_10: 112,
    Cyan_20: 113,
    Cyan_25: 114,
    Cyan_30: 115,
    Cyan_40: 116,
    Cyan_50: 117,
    Cyan_60: 118,
    Cyan_70: 119,
    Cyan_75: 120,
    Cyan_80: 121,
    Cyan_90: 122,

    Magenta_10: 123,
    Magenta_20: 124,
    Magenta_25: 125,
    Magenta_30: 126,
    Magenta_40: 127,
    Magenta_50: 128,
    Magenta_60: 129,
    Magenta_70: 130,
    Magenta_75: 131,
    Magenta_80: 132,
    Magenta_90: 133,

    Yellow_10: 134,
    Yellow_20: 135,
    Yellow_25: 136,
    Yellow_30: 137,
    Yellow_40: 138,
    Yellow_50: 139,
    Yellow_60: 140,
    Yellow_70: 141,
    Yellow_75: 142,
    Yellow_80: 143,
    Yellow_90: 144,

    Black_10: 145,
    Black_20: 146,
    Black_25: 147,
    Black_30: 148,
    Black_40: 149,
    Black_50: 150,
    Black_60: 151,
    Black_70: 152,
    Black_75: 153,
    Black_80: 154,
    Black_90: 155
}