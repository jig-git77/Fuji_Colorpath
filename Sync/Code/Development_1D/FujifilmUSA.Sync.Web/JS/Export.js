////*********** Declarations ***********////
var CURRENT_USER_ID
var SERVICE_BASE_URL = '';
var $hdnBaseServiceUrl;
var $CurrentCulture;
var $hdnOptimizationRunID;
var $drpCurveType;
//var response = { "data": { "CurveExportView": [{ "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 0, "CMYK_C": 0.00, "CMYK_M": 0.00, "CMYK_Y": 0.00, "CMYK_K": 0.00 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 1, "CMYK_C": 0.84, "CMYK_M": 0.62, "CMYK_Y": 0.64, "CMYK_K": 0.97 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 2, "CMYK_C": 1.68, "CMYK_M": 1.23, "CMYK_Y": 1.27, "CMYK_K": 1.94 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 3, "CMYK_C": 2.50, "CMYK_M": 1.82, "CMYK_Y": 1.88, "CMYK_K": 2.91 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 4, "CMYK_C": 3.33, "CMYK_M": 2.39, "CMYK_Y": 2.47, "CMYK_K": 3.89 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 5, "CMYK_C": 4.14, "CMYK_M": 2.96, "CMYK_Y": 3.06, "CMYK_K": 4.87 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 6, "CMYK_C": 4.95, "CMYK_M": 3.53, "CMYK_Y": 3.65, "CMYK_K": 5.86 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 7, "CMYK_C": 5.76, "CMYK_M": 4.10, "CMYK_Y": 4.24, "CMYK_K": 6.84 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 8, "CMYK_C": 6.56, "CMYK_M": 4.67, "CMYK_Y": 4.84, "CMYK_K": 7.83 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 9, "CMYK_C": 7.37, "CMYK_M": 5.26, "CMYK_Y": 5.45, "CMYK_K": 8.82 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 10, "CMYK_C": 8.18, "CMYK_M": 5.87, "CMYK_Y": 6.07, "CMYK_K": 9.81 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 11, "CMYK_C": 8.99, "CMYK_M": 6.50, "CMYK_Y": 6.72, "CMYK_K": 10.79 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 12, "CMYK_C": 9.81, "CMYK_M": 7.16, "CMYK_Y": 7.40, "CMYK_K": 11.78 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 13, "CMYK_C": 10.63, "CMYK_M": 7.85, "CMYK_Y": 8.11, "CMYK_K": 12.76 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 14, "CMYK_C": 11.46, "CMYK_M": 8.60, "CMYK_Y": 8.87, "CMYK_K": 13.74 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 15, "CMYK_C": 12.30, "CMYK_M": 9.40, "CMYK_Y": 9.68, "CMYK_K": 14.71 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 16, "CMYK_C": 13.15, "CMYK_M": 10.29, "CMYK_Y": 10.57, "CMYK_K": 15.68 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 17, "CMYK_C": 14.01, "CMYK_M": 11.32, "CMYK_Y": 11.59, "CMYK_K": 16.64 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 18, "CMYK_C": 14.89, "CMYK_M": 12.63, "CMYK_Y": 12.85, "CMYK_K": 17.59 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 19, "CMYK_C": 15.79, "CMYK_M": 14.17, "CMYK_Y": 14.32, "CMYK_K": 18.54 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 20, "CMYK_C": 16.71, "CMYK_M": 15.92, "CMYK_Y": 15.96, "CMYK_K": 19.47 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 21, "CMYK_C": 17.64, "CMYK_M": 17.71, "CMYK_Y": 17.66, "CMYK_K": 20.40 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 22, "CMYK_C": 18.60, "CMYK_M": 19.39, "CMYK_Y": 19.29, "CMYK_K": 21.32 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 23, "CMYK_C": 19.62, "CMYK_M": 20.83, "CMYK_Y": 20.73, "CMYK_K": 22.23 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 24, "CMYK_C": 22.45, "CMYK_M": 22.01, "CMYK_Y": 21.95, "CMYK_K": 23.13 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 25, "CMYK_C": 25.02, "CMYK_M": 22.93, "CMYK_Y": 22.94, "CMYK_K": 24.01 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 26, "CMYK_C": 25.76, "CMYK_M": 23.52, "CMYK_Y": 23.62, "CMYK_K": 24.87 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 27, "CMYK_C": 26.39, "CMYK_M": 23.84, "CMYK_Y": 24.05, "CMYK_K": 25.70 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 28, "CMYK_C": 27.00, "CMYK_M": 24.03, "CMYK_Y": 24.35, "CMYK_K": 26.51 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 29, "CMYK_C": 27.54, "CMYK_M": 24.15, "CMYK_Y": 24.59, "CMYK_K": 27.28 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 30, "CMYK_C": 28.06, "CMYK_M": 24.24, "CMYK_Y": 24.81, "CMYK_K": 28.05 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 31, "CMYK_C": 28.54, "CMYK_M": 24.33, "CMYK_Y": 25.06, "CMYK_K": 28.78 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 32, "CMYK_C": 29.01, "CMYK_M": 24.46, "CMYK_Y": 25.37, "CMYK_K": 29.51 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 33, "CMYK_C": 29.46, "CMYK_M": 24.65, "CMYK_Y": 25.81, "CMYK_K": 30.23 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 34, "CMYK_C": 29.91, "CMYK_M": 24.99, "CMYK_Y": 26.55, "CMYK_K": 30.95 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 35, "CMYK_C": 30.36, "CMYK_M": 25.65, "CMYK_Y": 27.59, "CMYK_K": 31.66 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 36, "CMYK_C": 30.81, "CMYK_M": 26.65, "CMYK_Y": 28.78, "CMYK_K": 32.37 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 37, "CMYK_C": 31.26, "CMYK_M": 27.83, "CMYK_Y": 30.05, "CMYK_K": 33.08 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 38, "CMYK_C": 31.74, "CMYK_M": 29.14, "CMYK_Y": 31.43, "CMYK_K": 33.79 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 39, "CMYK_C": 32.23, "CMYK_M": 30.60, "CMYK_Y": 32.93, "CMYK_K": 34.51 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 40, "CMYK_C": 32.76, "CMYK_M": 32.20, "CMYK_Y": 34.49, "CMYK_K": 35.25 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 41, "CMYK_C": 33.31, "CMYK_M": 33.92, "CMYK_Y": 36.10, "CMYK_K": 36.00 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 42, "CMYK_C": 33.92, "CMYK_M": 35.69, "CMYK_Y": 37.72, "CMYK_K": 36.75 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 43, "CMYK_C": 34.58, "CMYK_M": 37.45, "CMYK_Y": 39.31, "CMYK_K": 37.55 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 44, "CMYK_C": 35.33, "CMYK_M": 39.15, "CMYK_Y": 40.83, "CMYK_K": 38.35 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 45, "CMYK_C": 36.18, "CMYK_M": 40.75, "CMYK_Y": 42.28, "CMYK_K": 39.20 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 46, "CMYK_C": 37.42, "CMYK_M": 42.20, "CMYK_Y": 43.63, "CMYK_K": 40.07 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 47, "CMYK_C": 39.71, "CMYK_M": 43.47, "CMYK_Y": 44.85, "CMYK_K": 41.00 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 48, "CMYK_C": 43.23, "CMYK_M": 44.61, "CMYK_Y": 45.95, "CMYK_K": 41.97 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 49, "CMYK_C": 45.96, "CMYK_M": 45.61, "CMYK_Y": 46.96, "CMYK_K": 43.01 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 50, "CMYK_C": 47.47, "CMYK_M": 46.33, "CMYK_Y": 47.80, "CMYK_K": 44.11 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 51, "CMYK_C": 48.58, "CMYK_M": 46.42, "CMYK_Y": 48.37, "CMYK_K": 45.30 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 52, "CMYK_C": 49.56, "CMYK_M": 46.46, "CMYK_Y": 48.61, "CMYK_K": 46.56 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 53, "CMYK_C": 50.54, "CMYK_M": 46.52, "CMYK_Y": 48.63, "CMYK_K": 47.82 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 54, "CMYK_C": 51.43, "CMYK_M": 46.57, "CMYK_Y": 48.65, "CMYK_K": 49.23 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 55, "CMYK_C": 52.31, "CMYK_M": 46.67, "CMYK_Y": 48.67, "CMYK_K": 50.65 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 56, "CMYK_C": 53.14, "CMYK_M": 46.76, "CMYK_Y": 48.70, "CMYK_K": 52.15 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 57, "CMYK_C": 53.96, "CMYK_M": 46.84, "CMYK_Y": 48.72, "CMYK_K": 53.70 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 58, "CMYK_C": 54.74, "CMYK_M": 46.93, "CMYK_Y": 48.74, "CMYK_K": 55.30 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 59, "CMYK_C": 55.53, "CMYK_M": 47.00, "CMYK_Y": 48.76, "CMYK_K": 56.95 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 60, "CMYK_C": 56.30, "CMYK_M": 47.06, "CMYK_Y": 48.77, "CMYK_K": 58.62 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 61, "CMYK_C": 57.06, "CMYK_M": 47.12, "CMYK_Y": 48.98, "CMYK_K": 60.33 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 62, "CMYK_C": 57.83, "CMYK_M": 47.15, "CMYK_Y": 49.69, "CMYK_K": 62.05 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 63, "CMYK_C": 58.59, "CMYK_M": 47.31, "CMYK_Y": 50.98, "CMYK_K": 63.78 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 64, "CMYK_C": 59.37, "CMYK_M": 48.43, "CMYK_Y": 52.54, "CMYK_K": 65.49 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 65, "CMYK_C": 60.15, "CMYK_M": 49.90, "CMYK_Y": 54.41, "CMYK_K": 67.18 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 66, "CMYK_C": 60.95, "CMYK_M": 51.73, "CMYK_Y": 56.60, "CMYK_K": 68.86 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 67, "CMYK_C": 61.78, "CMYK_M": 53.92, "CMYK_Y": 59.14, "CMYK_K": 70.47 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 68, "CMYK_C": 62.62, "CMYK_M": 56.54, "CMYK_Y": 62.02, "CMYK_K": 72.07 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 69, "CMYK_C": 63.52, "CMYK_M": 59.57, "CMYK_Y": 65.07, "CMYK_K": 73.57 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 70, "CMYK_C": 64.43, "CMYK_M": 62.79, "CMYK_Y": 68.05, "CMYK_K": 75.06 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 71, "CMYK_C": 65.43, "CMYK_M": 65.89, "CMYK_Y": 70.76, "CMYK_K": 76.44 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 72, "CMYK_C": 66.45, "CMYK_M": 68.60, "CMYK_Y": 73.11, "CMYK_K": 77.79 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 73, "CMYK_C": 67.61, "CMYK_M": 70.89, "CMYK_Y": 75.12, "CMYK_K": 79.07 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 74, "CMYK_C": 72.55, "CMYK_M": 72.78, "CMYK_Y": 76.84, "CMYK_K": 80.26 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 75, "CMYK_C": 76.59, "CMYK_M": 74.32, "CMYK_Y": 78.26, "CMYK_K": 81.45 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 76, "CMYK_C": 77.81, "CMYK_M": 75.74, "CMYK_Y": 79.56, "CMYK_K": 82.51 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 77, "CMYK_C": 78.92, "CMYK_M": 77.03, "CMYK_Y": 80.73, "CMYK_K": 83.53 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 78, "CMYK_C": 80.02, "CMYK_M": 78.28, "CMYK_Y": 81.83, "CMYK_K": 84.45 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 79, "CMYK_C": 81.06, "CMYK_M": 79.45, "CMYK_Y": 82.87, "CMYK_K": 85.35 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 80, "CMYK_C": 82.09, "CMYK_M": 80.60, "CMYK_Y": 83.86, "CMYK_K": 86.18 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 81, "CMYK_C": 83.06, "CMYK_M": 81.68, "CMYK_Y": 84.79, "CMYK_K": 87.00 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 82, "CMYK_C": 84.03, "CMYK_M": 82.75, "CMYK_Y": 85.70, "CMYK_K": 87.77 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 83, "CMYK_C": 84.97, "CMYK_M": 83.77, "CMYK_Y": 86.56, "CMYK_K": 88.52 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 84, "CMYK_C": 85.90, "CMYK_M": 84.79, "CMYK_Y": 87.41, "CMYK_K": 89.24 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 85, "CMYK_C": 86.80, "CMYK_M": 85.76, "CMYK_Y": 88.22, "CMYK_K": 89.94 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 86, "CMYK_C": 87.70, "CMYK_M": 86.73, "CMYK_Y": 89.03, "CMYK_K": 90.64 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 87, "CMYK_C": 88.58, "CMYK_M": 87.69, "CMYK_Y": 89.82, "CMYK_K": 91.30 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 88, "CMYK_C": 89.46, "CMYK_M": 88.63, "CMYK_Y": 90.60, "CMYK_K": 91.97 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 89, "CMYK_C": 90.33, "CMYK_M": 89.56, "CMYK_Y": 91.37, "CMYK_K": 92.63 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 90, "CMYK_C": 91.19, "CMYK_M": 90.49, "CMYK_Y": 92.13, "CMYK_K": 93.28 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 91, "CMYK_C": 92.05, "CMYK_M": 91.41, "CMYK_Y": 92.88, "CMYK_K": 93.92 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 92, "CMYK_C": 92.91, "CMYK_M": 92.34, "CMYK_Y": 93.64, "CMYK_K": 94.57 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 93, "CMYK_C": 93.78, "CMYK_M": 93.26, "CMYK_Y": 94.40, "CMYK_K": 95.22 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 94, "CMYK_C": 94.64, "CMYK_M": 94.19, "CMYK_Y": 95.17, "CMYK_K": 95.87 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 95, "CMYK_C": 95.51, "CMYK_M": 95.13, "CMYK_Y": 95.94, "CMYK_K": 96.53 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 96, "CMYK_C": 96.39, "CMYK_M": 96.07, "CMYK_Y": 96.72, "CMYK_K": 97.20 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 97, "CMYK_C": 97.28, "CMYK_M": 97.04, "CMYK_Y": 97.52, "CMYK_K": 97.88 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 98, "CMYK_C": 98.17, "CMYK_M": 98.00, "CMYK_Y": 98.32, "CMYK_K": 98.57 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 99, "CMYK_C": 99.08, "CMYK_M": 99.00, "CMYK_Y": 99.15, "CMYK_K": 99.28 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 100, "CMYK_C": 100.00, "CMYK_M": 100.00, "CMYK_Y": 100.00, "CMYK_K": 100.00 }], "CurveDeltaRefView": [{ "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 0, "CMYK_C": 0.00, "CMYK_M": 0.00, "CMYK_Y": 0.00, "CMYK_K": 0.00 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 1, "CMYK_C": -0.16, "CMYK_M": -0.38, "CMYK_Y": -0.36, "CMYK_K": -0.03 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 2, "CMYK_C": -0.32, "CMYK_M": -0.77, "CMYK_Y": -0.73, "CMYK_K": -0.06 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 3, "CMYK_C": -0.50, "CMYK_M": -1.18, "CMYK_Y": -1.12, "CMYK_K": -0.09 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 4, "CMYK_C": -0.67, "CMYK_M": -1.61, "CMYK_Y": -1.53, "CMYK_K": -0.11 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 5, "CMYK_C": -0.86, "CMYK_M": -2.04, "CMYK_Y": -1.94, "CMYK_K": -0.13 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 6, "CMYK_C": -1.05, "CMYK_M": -2.47, "CMYK_Y": -2.35, "CMYK_K": -0.14 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 7, "CMYK_C": -1.24, "CMYK_M": -2.90, "CMYK_Y": -2.76, "CMYK_K": -0.16 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 8, "CMYK_C": -1.44, "CMYK_M": -3.33, "CMYK_Y": -3.16, "CMYK_K": -0.17 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 9, "CMYK_C": -1.63, "CMYK_M": -3.74, "CMYK_Y": -3.55, "CMYK_K": -0.18 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 10, "CMYK_C": -1.82, "CMYK_M": -4.13, "CMYK_Y": -3.93, "CMYK_K": -0.19 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 11, "CMYK_C": -2.01, "CMYK_M": -4.50, "CMYK_Y": -4.28, "CMYK_K": -0.21 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 12, "CMYK_C": -2.19, "CMYK_M": -4.84, "CMYK_Y": -4.60, "CMYK_K": -0.22 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 13, "CMYK_C": -2.37, "CMYK_M": -5.15, "CMYK_Y": -4.89, "CMYK_K": -0.24 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 14, "CMYK_C": -2.54, "CMYK_M": -5.40, "CMYK_Y": -5.13, "CMYK_K": -0.26 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 15, "CMYK_C": -2.70, "CMYK_M": -5.60, "CMYK_Y": -5.32, "CMYK_K": -0.29 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 16, "CMYK_C": -2.85, "CMYK_M": -5.71, "CMYK_Y": -5.43, "CMYK_K": -0.32 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 17, "CMYK_C": -2.99, "CMYK_M": -5.68, "CMYK_Y": -5.41, "CMYK_K": -0.36 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 18, "CMYK_C": -3.11, "CMYK_M": -5.37, "CMYK_Y": -5.15, "CMYK_K": -0.41 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 19, "CMYK_C": -3.21, "CMYK_M": -4.83, "CMYK_Y": -4.68, "CMYK_K": -0.46 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 20, "CMYK_C": -3.29, "CMYK_M": -4.08, "CMYK_Y": -4.04, "CMYK_K": -0.53 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 21, "CMYK_C": -3.36, "CMYK_M": -3.29, "CMYK_Y": -3.34, "CMYK_K": -0.60 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 22, "CMYK_C": -3.40, "CMYK_M": -2.61, "CMYK_Y": -2.71, "CMYK_K": -0.68 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 23, "CMYK_C": -3.38, "CMYK_M": -2.17, "CMYK_Y": -2.27, "CMYK_K": -0.77 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 24, "CMYK_C": -1.55, "CMYK_M": -1.99, "CMYK_Y": -2.05, "CMYK_K": -0.87 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 25, "CMYK_C": 0.02, "CMYK_M": -2.07, "CMYK_Y": -2.06, "CMYK_K": -0.99 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 26, "CMYK_C": -0.24, "CMYK_M": -2.48, "CMYK_Y": -2.38, "CMYK_K": -1.13 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 27, "CMYK_C": -0.61, "CMYK_M": -3.16, "CMYK_Y": -2.95, "CMYK_K": -1.30 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 28, "CMYK_C": -1.00, "CMYK_M": -3.97, "CMYK_Y": -3.65, "CMYK_K": -1.49 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 29, "CMYK_C": -1.46, "CMYK_M": -4.85, "CMYK_Y": -4.41, "CMYK_K": -1.72 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 30, "CMYK_C": -1.94, "CMYK_M": -5.76, "CMYK_Y": -5.19, "CMYK_K": -1.95 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 31, "CMYK_C": -2.46, "CMYK_M": -6.67, "CMYK_Y": -5.94, "CMYK_K": -2.22 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 32, "CMYK_C": -2.99, "CMYK_M": -7.54, "CMYK_Y": -6.63, "CMYK_K": -2.49 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 33, "CMYK_C": -3.54, "CMYK_M": -8.35, "CMYK_Y": -7.19, "CMYK_K": -2.77 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 34, "CMYK_C": -4.09, "CMYK_M": -9.01, "CMYK_Y": -7.45, "CMYK_K": -3.05 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 35, "CMYK_C": -4.64, "CMYK_M": -9.35, "CMYK_Y": -7.41, "CMYK_K": -3.34 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 36, "CMYK_C": -5.19, "CMYK_M": -9.35, "CMYK_Y": -7.22, "CMYK_K": -3.63 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 37, "CMYK_C": -5.74, "CMYK_M": -9.17, "CMYK_Y": -6.95, "CMYK_K": -3.92 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 38, "CMYK_C": -6.26, "CMYK_M": -8.86, "CMYK_Y": -6.57, "CMYK_K": -4.21 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 39, "CMYK_C": -6.77, "CMYK_M": -8.40, "CMYK_Y": -6.07, "CMYK_K": -4.49 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 40, "CMYK_C": -7.24, "CMYK_M": -7.80, "CMYK_Y": -5.51, "CMYK_K": -4.75 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 41, "CMYK_C": -7.69, "CMYK_M": -7.08, "CMYK_Y": -4.90, "CMYK_K": -5.00 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 42, "CMYK_C": -8.08, "CMYK_M": -6.31, "CMYK_Y": -4.28, "CMYK_K": -5.25 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 43, "CMYK_C": -8.42, "CMYK_M": -5.55, "CMYK_Y": -3.69, "CMYK_K": -5.45 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 44, "CMYK_C": -8.67, "CMYK_M": -4.85, "CMYK_Y": -3.17, "CMYK_K": -5.65 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 45, "CMYK_C": -8.82, "CMYK_M": -4.25, "CMYK_Y": -2.72, "CMYK_K": -5.80 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 46, "CMYK_C": -8.58, "CMYK_M": -3.80, "CMYK_Y": -2.37, "CMYK_K": -5.93 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 47, "CMYK_C": -7.29, "CMYK_M": -3.53, "CMYK_Y": -2.15, "CMYK_K": -6.00 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 48, "CMYK_C": -4.77, "CMYK_M": -3.39, "CMYK_Y": -2.05, "CMYK_K": -6.03 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 49, "CMYK_C": -3.04, "CMYK_M": -3.39, "CMYK_Y": -2.04, "CMYK_K": -5.99 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 50, "CMYK_C": -2.53, "CMYK_M": -3.67, "CMYK_Y": -2.20, "CMYK_K": -5.89 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 51, "CMYK_C": -2.42, "CMYK_M": -4.58, "CMYK_Y": -2.63, "CMYK_K": -5.70 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 52, "CMYK_C": -2.44, "CMYK_M": -5.54, "CMYK_Y": -3.39, "CMYK_K": -5.44 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 53, "CMYK_C": -2.46, "CMYK_M": -6.48, "CMYK_Y": -4.37, "CMYK_K": -5.18 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 54, "CMYK_C": -2.57, "CMYK_M": -7.43, "CMYK_Y": -5.35, "CMYK_K": -4.77 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 55, "CMYK_C": -2.69, "CMYK_M": -8.33, "CMYK_Y": -6.33, "CMYK_K": -4.35 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 56, "CMYK_C": -2.86, "CMYK_M": -9.24, "CMYK_Y": -7.30, "CMYK_K": -3.85 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 57, "CMYK_C": -3.04, "CMYK_M": -10.16, "CMYK_Y": -8.28, "CMYK_K": -3.30 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 58, "CMYK_C": -3.26, "CMYK_M": -11.07, "CMYK_Y": -9.26, "CMYK_K": -2.70 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 59, "CMYK_C": -3.47, "CMYK_M": -12.00, "CMYK_Y": -10.24, "CMYK_K": -2.05 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 60, "CMYK_C": -3.70, "CMYK_M": -12.94, "CMYK_Y": -11.23, "CMYK_K": -1.38 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 61, "CMYK_C": -3.94, "CMYK_M": -13.88, "CMYK_Y": -12.02, "CMYK_K": -0.67 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 62, "CMYK_C": -4.17, "CMYK_M": -14.85, "CMYK_Y": -12.31, "CMYK_K": 0.05 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 63, "CMYK_C": -4.41, "CMYK_M": -15.69, "CMYK_Y": -12.02, "CMYK_K": 0.78 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 64, "CMYK_C": -4.63, "CMYK_M": -15.57, "CMYK_Y": -11.46, "CMYK_K": 1.49 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 65, "CMYK_C": -4.85, "CMYK_M": -15.10, "CMYK_Y": -10.59, "CMYK_K": 2.18 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 66, "CMYK_C": -5.05, "CMYK_M": -14.27, "CMYK_Y": -9.40, "CMYK_K": 2.86 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 67, "CMYK_C": -5.22, "CMYK_M": -13.08, "CMYK_Y": -7.86, "CMYK_K": 3.47 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 68, "CMYK_C": -5.38, "CMYK_M": -11.46, "CMYK_Y": -5.98, "CMYK_K": 4.07 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 69, "CMYK_C": -5.48, "CMYK_M": -9.43, "CMYK_Y": -3.93, "CMYK_K": 4.57 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 70, "CMYK_C": -5.57, "CMYK_M": -7.21, "CMYK_Y": -1.95, "CMYK_K": 5.06 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 71, "CMYK_C": -5.57, "CMYK_M": -5.11, "CMYK_Y": -0.24, "CMYK_K": 5.44 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 72, "CMYK_C": -5.55, "CMYK_M": -3.40, "CMYK_Y": 1.11, "CMYK_K": 5.79 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 73, "CMYK_C": -5.39, "CMYK_M": -2.11, "CMYK_Y": 2.12, "CMYK_K": 6.07 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 74, "CMYK_C": -1.45, "CMYK_M": -1.22, "CMYK_Y": 2.84, "CMYK_K": 6.26 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 75, "CMYK_C": 1.59, "CMYK_M": -0.68, "CMYK_Y": 3.26, "CMYK_K": 6.45 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 76, "CMYK_C": 1.81, "CMYK_M": -0.26, "CMYK_Y": 3.56, "CMYK_K": 6.51 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 77, "CMYK_C": 1.92, "CMYK_M": 0.03, "CMYK_Y": 3.73, "CMYK_K": 6.53 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 78, "CMYK_C": 2.02, "CMYK_M": 0.28, "CMYK_Y": 3.83, "CMYK_K": 6.45 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 79, "CMYK_C": 2.06, "CMYK_M": 0.45, "CMYK_Y": 3.87, "CMYK_K": 6.35 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 80, "CMYK_C": 2.09, "CMYK_M": 0.60, "CMYK_Y": 3.86, "CMYK_K": 6.18 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 81, "CMYK_C": 2.06, "CMYK_M": 0.68, "CMYK_Y": 3.79, "CMYK_K": 6.00 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 82, "CMYK_C": 2.03, "CMYK_M": 0.75, "CMYK_Y": 3.70, "CMYK_K": 5.77 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 83, "CMYK_C": 1.97, "CMYK_M": 0.77, "CMYK_Y": 3.56, "CMYK_K": 5.52 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 84, "CMYK_C": 1.90, "CMYK_M": 0.79, "CMYK_Y": 3.41, "CMYK_K": 5.24 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 85, "CMYK_C": 1.80, "CMYK_M": 0.76, "CMYK_Y": 3.22, "CMYK_K": 4.94 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 86, "CMYK_C": 1.70, "CMYK_M": 0.73, "CMYK_Y": 3.03, "CMYK_K": 4.64 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 87, "CMYK_C": 1.58, "CMYK_M": 0.69, "CMYK_Y": 2.82, "CMYK_K": 4.30 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 88, "CMYK_C": 1.46, "CMYK_M": 0.63, "CMYK_Y": 2.60, "CMYK_K": 3.97 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 89, "CMYK_C": 1.33, "CMYK_M": 0.56, "CMYK_Y": 2.37, "CMYK_K": 3.63 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 90, "CMYK_C": 1.19, "CMYK_M": 0.49, "CMYK_Y": 2.13, "CMYK_K": 3.28 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 91, "CMYK_C": 1.05, "CMYK_M": 0.41, "CMYK_Y": 1.88, "CMYK_K": 2.92 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 92, "CMYK_C": 0.91, "CMYK_M": 0.34, "CMYK_Y": 1.64, "CMYK_K": 2.57 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 93, "CMYK_C": 0.78, "CMYK_M": 0.26, "CMYK_Y": 1.40, "CMYK_K": 2.22 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 94, "CMYK_C": 0.64, "CMYK_M": 0.19, "CMYK_Y": 1.17, "CMYK_K": 1.87 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 95, "CMYK_C": 0.51, "CMYK_M": 0.13, "CMYK_Y": 0.94, "CMYK_K": 1.53 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 96, "CMYK_C": 0.39, "CMYK_M": 0.07, "CMYK_Y": 0.72, "CMYK_K": 1.20 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 97, "CMYK_C": 0.28, "CMYK_M": 0.04, "CMYK_Y": 0.52, "CMYK_K": 0.88 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 98, "CMYK_C": 0.17, "CMYK_M": 0.00, "CMYK_Y": 0.32, "CMYK_K": 0.57 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 99, "CMYK_C": 0.08, "CMYK_M": 0.00, "CMYK_Y": 0.15, "CMYK_K": 0.28 }, { "OptimizeRunCurveID": 0, "OptimizeRunID": 0, "ReferenceNo": 100, "CMYK_C": 0.00, "CMYK_M": 0.00, "CMYK_Y": 0.00, "CMYK_K": 0.00 }], "CurveDeltaPrevRunView": [], "GlobalResources": { "_recgbPatchMetrics": "% GB Patch Metrics", "_recActual": "Actual", "_recL": "L*", "_recA": "a*", "_recB": "b*", "_recDeltaE": "ΔE", "_recTarget": "Target", "_recPrevious": "Previous", "_recDeltaL": "ΔL*", "_recDeltaA": "Δa*", "_recDeltaB": "Δb*", "_recPreDeltaE": "Previous ΔE", "_recCyanPathMetrics": "Cyan Patch Metrics", "_recMagentaPathMetrics": "Magenta Patch Metrics", "_recYellowPathMetrics": "Yellow Patch Metrics", "_recRedPathMetrics": "Red Patch Metrics", "_recGreenPathMetrics": "Green Patch Metrics", "_recBluePathMetrics": "Blue Patch Metrics", "_recBlackPathMetrics": "Black Patch Metrics", "_recPaperPathMetrics": "Paper Patch Metrics", "_recCyan": "Cyan", "_recMagenta": "Magenta", "_recYellow": "Yellow", "_recBlack": "Black", "_recRed": "Red", "_recGreen": "Green", "_recBlue": "Blue", "_recPaper": "Paper", "_recTriplet": "3-C", "_recTripletPathMetrics": "3-C Patch Metrics", "_recLBalanceChartTitle": "L* Balance Gray Chart", "_recABalanceChartTitle": "a* Balance Gray Chart", "_recBBalanceChartTitle": "b* Balance Gray Chart", "gridAnalyzeHeaderIDResource": "ID", "gridAnalyzeHeaderPatchResource": "Patch", "gridAnalyzeHeaderViewResource": "View", "gridAnalyzeHeaderSpotNameResource": "Spot Name", "gridAnalyzeHeaderETPResource": "ΔE_T_P", "gridAnalyzeHeaderEMTResource": "ΔE_M_T", "gridAnalyzeHeaderEMPResource": "ΔE_M_P", "gridAnalyzeHeaderDeltaLResource": "pΔL", "gridAnalyzeHeaderDeltaAResource": "pΔa", "gridAnalyzeHeaderDeltaBesource": "pΔb", "gridAnalyzeHeaderDeltaEResource": "pΔE", "gridAnalyzeToolTipViewResource": "View Patch Details", "gridAnalyzeHeader_C_Resource": "C", "gridAnalyzeHeader_M_Resource": "M", "gridAnalyzeHeader_Y_Resource": "Y", "gridAnalyzeHeader_K_Resource": "K", "gridAnalyzeHeader_E_Resource": "ΔE", "gridAnalyzeHeaderE94Resource": "ΔE94", "gridAnalyzeHeaderE2000Resource": "ΔE2000", "gridAnalyzeHeaderTargetLResource": "Target L*", "gridAnalyzeHeaderCIELAB_LResource": "Actual L*", "gridAnalyzeHeaderTargetAResource": "Target a*", "gridAnalyzeHeaderCIELAB_AResource": "Actual a*", "gridAnalyzeHeaderTargetBResource": "Target b*", "gridAnalyzeHeaderCIELAB_BResource": "Actual b*", "gridAnalyzeHeaderDensity_VResource": "Density_V", "gridAnalyzeHeaderDensity_CResource": "Density_C", "gridAnalyzeHeaderDensity_MResource": "Density_M", "gridAnalyzeHeaderDensity_YResource": "Density_Y", "gridAnalyzeHeaderDelta_HResource": "ΔH", "gridAnalyzeHeaderDelta_CHResource": "ΔCh", "gridAnalyzeHeaderDelta_LResource": "ΔL", "_recPatchMetrics": "Patch Metrics", "_recactualTVI": "Actual TVI:", "_rectargetTVI": "Target TVI:", "_recTVIThr": "TVI Threashold", "_recpercentPatchMetrics": "% Patch Metrics", "selectMeasurementMsg": "Atleast one measurement should be checked" } }, "error": { "userMessage": "", "internalMessage": "", "code": 0 } };
var _Resources = {};


//********** kendo objects *********//
var drawing = kendo.drawing;
var geometry = kendo.geometry;


//********** Gradient kendo objects *********//

var gradientGray = new drawing.LinearGradient({
    start: [0, 0], // Bottom left
    end: [0, 1],   // Top left
    stops: [{
        offset: 0,
        color: "#000",
        opacity: 0.2
    }, {
        offset: 1,
        color: "#000",
        opacity: 0
    }]
});


$(document).ready(function () {

    $hdnBaseServiceUrl = $('#hdnBaseServiceUrl').val();
    $CurrentCulture = $("#hdfCurrentCulture").val();
    $drpCurveType = $("#drpCurveType");
    SERVICE_BASE_URL = $hdnBaseServiceUrl;
    $hdnOptimizationRunID = $('#hdnOptimizationRunID').val();



    $drpCurveType.kendoDropDownList({
        height: 1000        
    });



    BindChart();
    $drpCurveType.on('change', function (e) {
        e.preventDefault();
        $('#HD_CurveType').val($(this).val());
        BindChart();
    });
    $("#tabstrip").kendoTabStrip({
        animation: {
            open: {
                effects: "fadeIn"
            }
        }
    });


    $('#BtnExportCurvesExportView').on('click', function (e) {
        e.preventDefault();
        var curveTypeID = parseInt($drpCurveType.val());

        var apiParam = {
            OptimizationRunId: parseInt($hdnOptimizationRunID),
            CurveType: curveTypeID
        };

        $.ajax({
            type: "GET",
            url: SERVICE_BASE_URL + 'api/Chart/ExportGenerateCurveChart',
            data: apiParam,
            dataType: "json",
            success: function (response) {
                if (response.data != null) {
                    console.log(response.data);
                    var fileName = '~' + response.data;
                    window.location.href = SERVICE_BASE_URL + '/api/Analyze/DownloadFile?filePath=' + fileName;
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                console.log(XMLHttpRequest.responseText);
            }
        });

    });
});

function BindChart() {

    var curveTypeID = parseInt($drpCurveType.val());
    //---Set same value for "ColorGate-TVI" and "ColorGate-CGCX" like "5%" for export curve data
    if (curveTypeID == 100 || curveTypeID == 101) {
        curveTypeID = 5;
    }

    //---Set same value for "Onyx G7 Import" like "Nexus" for export curve data
    if (curveTypeID == 102) {
        curveTypeID = 45;
    }

    var apiParameters = {
        OptimizationRunId: $hdnOptimizationRunID,
        CurveType: curveTypeID,
    };



    $.ajax({
        url: SERVICE_BASE_URL + 'api/Chart/GenerateCurveChart',
        data: apiParameters,
        type: 'GET',
        dataType: 'json',
        headers: {
            'Accept-Language': $CurrentCulture
        },
        success: function (response) {
            if (response.data != null) {
                _Resources = response.data.GlobalResources;
                BindExportView(response.data);
                BindDeltaView(response.data);
            }
        },
        error: function (xhr, errorType, exception) {
            kendo.ui.progress($chartLoader, false);
        }
    });
}

function BindExportView(result) {

    var kendoGrid = $("#tblExport").data("kendoGrid");

    $("#tblExport").kendoGrid({
        dataSource: {
            data: result.CurveExportView
        },
        schema: {
            model: {
                fields: {
                    ReferenceNo: {
                        from: "ReferenceNo",
                        type: "number",
                    },
                    CMYK_C: {
                        from: "CMYK_C",
                        type: "number",
                        format: "{n4}"
                    },
                    CMYK_M: {
                        from: "CMYK_M",
                        type: "number",
                        format: "{n4}"
                    },
                    CMYK_Y: {
                        from: "CMYK_Y",
                        type: "number",
                        format: "{n4}"
                    },
                    CMYK_K: {
                        from: "CMYK_K",
                        type: "number",
                        format: "{n4}"
                    },
                }
            }
        },
        pageable: false,
        scrollable: true,
        height: 400,
        columns: [
            {
                field: "ReferenceNo",
                filterable: false,
                title: "<center><b title='Reference%'>Reference%</b></center>",
                width: '85px',
            },
            {
                field: "CMYK_C",
                filterable: false,
                title: "<center><b title='C'>C</b></center>",
                width: '41px'
            },
            {
                field: "CMYK_M",
                filterable: false,
                title: "<center><b title='M'>M</b></center>",
                width: '41px'
            },
            {
                field: "CMYK_Y",
                filterable: false,
                title: "<center><b title='Y'>Y</b></center>",
                width: '41px'
            },
            {
                field: "CMYK_K",
                filterable: false,
                title: "<center><b title='K'>K</b></center>",
                width: '41px'
            }
        ],
        dataBound: function (e) {
            $(e.sender.tbody).find("tr td:nth-child(2)").css('background-color', '#E0F3FF');
            $(e.sender.tbody).find("tr td:nth-child(3)").css('background-color', '#FFEFF9');
            $(e.sender.tbody).find("tr td:nth-child(4)").css('background-color', '#FFFDEE');
            $(e.sender.tbody).find("tr td:nth-child(5)").css('background-color', '#E7EBEE');
        }
    });



    var listC = {
        name: "Cyan",
        color: "cyan",
        markers: { background: "black", border: 0.1, size: 3 },
        data: []
    };
    var listY = {
        name: "Yellow",
        color: "yellow",
        markers: { background: "black", border: 0.1, size: 3 },
        data: []
    };
    var listM = {
        name: "Magenta",
        color: "magenta",
        markers: { background: "black", border: 0.1, size: 3 },
        data: []
    };
    var listB = {
        name: "Black",
        color: "black",
        markers: { background: "black", border: 0.1, size: 3 },
        data: []
    };

    $.each(result.CurveExportView, function (index, obj) {
        listC.data.push([obj.ReferenceNo, obj.CMYK_C.toFixed(0)]);
        listM.data.push([obj.ReferenceNo, obj.CMYK_M.toFixed(0)]);
        listY.data.push([obj.ReferenceNo, obj.CMYK_Y.toFixed(0)]);
        listB.data.push([obj.ReferenceNo, obj.CMYK_K.toFixed(0)]);
    });




    $("#Chart_Curve").kendoChart({
        renderAs: "canvas",
        //title: {
        //    text: "Export View"
        //},
        legend: {
            visible: true,
            position: 'top'
        },
        seriesDefaults: {
            type: "scatterLine"
        },
        series: [listC, listY, listM, listB],
        xAxis: {
            name: "xAxis",
            min: 0,
            max: 100,
            majorUnit: 25,
            labels: {
                format: "{0}"
            }
        },
        yAxis: {
            name: "yAxis",
            min: 0,
            max: 100,
            majorUnit: 10,
            labels: {
                format: "{0}"
            },

        },
        //tooltip: {
        //    visible: true,
        //    format: "{1}% in {0} minutes"
        //},
        //chartArea: {
        //    background: "#EFEFEF"
        //    //width: 500,
        //    //height: 400
        //},
        render: renderChartWithBackgroundByAxis
    });
}

function BindDeltaView(result) {

    //var kendoGrid = $("#tblDeltaView").data("kendoGrid");
    //kendo.bind($("#tblDeltaView"), result);

    //if (kendoGrid) {
    //}
    //else {
    //    $("#tblDeltaView").kendoGrid({
    //        pageable: false,
    //        scrollable: true,
    //        height: 400,
    //        dataBound: function (e) {
    //            $(e.sender.tbody).find("tr td:nth-child(2)").css('background-color', '#E0F3FF');
    //            $(e.sender.tbody).find("tr td:nth-child(3)").css('background-color', '#FFEFF9');
    //            $(e.sender.tbody).find("tr td:nth-child(4)").css('background-color', '#FFFDEE');
    //            $(e.sender.tbody).find("tr td:nth-child(5)").css('background-color', '#E7EBEE');
    //        }
    //    });
    //}

    $("#tblDeltaView").kendoGrid({
        dataSource: {
            data: result.CurveDeltaRefView
        },
        schema: {
            model: {
                fields: {
                    ReferenceNo: {
                        from: "ReferenceNo",
                        type: "number",
                    },
                    CMYK_C: {
                        from: "CMYK_C",
                        type: "number",
                        format: "{n4}"
                    },
                    CMYK_M: {
                        from: "CMYK_M",
                        type: "number",
                        format: "{n4}"
                    },
                    CMYK_Y: {
                        from: "CMYK_Y",
                        type: "number",
                        format: "{n4}"
                    },
                    CMYK_K: {
                        from: "CMYK_K",
                        type: "number",
                        format: "{n4}"
                    },
                }
            }
        },
        pageable: false,
        scrollable: true,
        height: 400,
        columns: [
            {
                field: "ReferenceNo",
                filterable: false,
                title: "<center><b title='Reference%'>Reference%</b></center>",
                width: '85px',
            },
            {
                field: "CMYK_C",
                filterable: false,
                title: "<center><b title='C'>C</b></center>",
                width: '41px'
            },
            {
                field: "CMYK_M",
                filterable: false,
                title: "<center><b title='M'>M</b></center>",
                width: '41px'
            },
            {
                field: "CMYK_Y",
                filterable: false,
                title: "<center><b title='Y'>Y</b></center>",
                width: '41px'
            },
            {
                field: "CMYK_K",
                filterable: false,
                title: "<center><b title='K'>K</b></center>",
                width: '41px'
            }
        ],
        dataBound: function (e) {
            $(e.sender.tbody).find("tr td:nth-child(2)").css('background-color', '#E0F3FF');
            $(e.sender.tbody).find("tr td:nth-child(3)").css('background-color', '#FFEFF9');
            $(e.sender.tbody).find("tr td:nth-child(4)").css('background-color', '#FFFDEE');
            $(e.sender.tbody).find("tr td:nth-child(5)").css('background-color', '#E7EBEE');
        }
    });

    var listC = {
        name: "Cyan",
        color: "cyan",
        markers: { background: "black", border: 0.1, size: 3, visible: false },
        data: []
    };
    var listY = {
        name: "Yellow",
        color: "yellow",
        markers: { background: "black", border: 0.1, size: 3, visible: false },
        data: []
    };
    var listM = {
        name: "Magenta",
        color: "magenta",
        markers: { background: "black", border: 0.1, size: 3, visible: false },
        data: []
    };
    var listB = {
        name: "Black",
        color: "black",
        markers: { background: "black", border: 0.1, size: 3, visible: false },
        data: []
    };

    $.each(result.CurveDeltaRefView, function (index, obj) {
        listC.data.push([obj.ReferenceNo, obj.CMYK_C.toFixed(0)]);
        listM.data.push([obj.ReferenceNo, obj.CMYK_M.toFixed(0)]);
        listY.data.push([obj.ReferenceNo, obj.CMYK_Y.toFixed(0)]);
        listB.data.push([obj.ReferenceNo, obj.CMYK_K.toFixed(0)]);
    });

    var minimumX = Math.min.apply(null, $.map(result.CurveDeltaRefView, function (e) {
        return Math.min(e.CMYK_C, e.CMYK_M, e.CMYK_Y, e.CMYK_K);
    }));

    var maximumX = Math.max.apply(null, $.map(result.CurveDeltaRefView, function (e) {
        return Math.max(e.CMYK_C, e.CMYK_M, e.CMYK_Y, e.CMYK_K);
    }));

    minimumX = parseInt(minimumX.toFixed(0)) - 1;
    maximumX = parseInt(maximumX.toFixed(0)) + 1;
    var division = (parseInt(minimumX.toFixed(0)) + parseInt(maximumX.toFixed(0))) / 5;

    $("#Chart_Delta").kendoChart({
        renderAs: "canvas",
        //title: {
        //    text: "ΔView"
        //},
        legend: {
            visible: true,
            position: 'top'
        },
        seriesDefaults: {
            type: "scatterLine",
            width: 2,
        },
        series: [listC, listY, listM, listB],
        xAxis: {
            name: "xAxis",
            min: 0,
            max: 100,
            majorUnit: 25,
            labels: {
                format: ""
            },
            line: {
                visible: false
            },
            axisCrossingValues: [-20, 100]
        },
        yAxis: {
            name: "yAxis",
            min: minimumX,
            max: maximumX,
            axisCrossingValues: [minimumX, 100]
            //min: minimumX,
            //max: maximumX,
            //majorUnit: division,
            //labels: {
            //    format: "{0}"
            //}
        },
        //tooltip: {
        //    visible: true,
        //    format: "{1}% in {0} minutes"
        //},
        //chartArea: {
        //    background: "#EFEFEF",
        //    width: 400,
        //    //height: 400
        //},
        render: renderChartWithBackgroundByAxis
    });
}

function renderChartWithBackgroundByAxis(e) {
    var xSlot = {};
    var ySlot = {};
    var path = {};
    var chart = e.sender;
    var xAxis = chart.getAxis("xAxis");
    var yAxis = chart.getAxis("yAxis");
    var xAxisArray = xAxis._axis.children.map(r => parseFloat(r.content));
    var yAxisArray = yAxis._axis.children.map(r => parseFloat(r.content));


    xSlot = xAxis.slot(Math.abs(Math.min.apply(Math, xAxisArray)), Math.abs(Math.max.apply(Math, xAxisArray)));
    ySlot = yAxis.slot(Math.abs(Math.min.apply(Math, yAxisArray)), Math.abs(Math.max.apply(Math, yAxisArray)));

    var yAxisHeight = ySlot.height();
    if (yAxisHeight == 0) {
        yAxisHeight = xSlot.origin.y - ySlot.origin.y
    }

    //set the rectangle area on chart
    var rect = new geometry.Rect([
        xSlot.origin.x, ySlot.origin.y
    ], [
            xSlot.width(), yAxisHeight
        ]);

    path = drawing.Path.fromRect(rect, {
        stroke: null,
        fill: gradientGray
    });

    chart.surface.draw(path);
}
