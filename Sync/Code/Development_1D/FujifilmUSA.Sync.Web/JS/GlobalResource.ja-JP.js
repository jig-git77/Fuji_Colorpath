/*
This file is used to display text which is displayed on UI using Javascript

Language: ja-JP
*/

//chartread.js file
var locErrorPlugin = "プラグインでエラーが発生しました。";
var locAlertOnFiredLoad = "load()からonfired()へ："; //"onfired() from load(): {0}_ja";
var locFujiCMPluginNotInstalled = "ステータス：富士フイルム色管理プラグインがインストールされていません。";
var locFujiCMPluginVersion = "富士フイルム色管理プラグインバージョン";  //"Fuji Color Manager Plugin Version {0}_ja";
var locFujiCMPluginIsOutdated = "ステータス：古いプラグインが検出されました。新しいプラグインをインストールしてください。";
var locScanningDeviceUnplugged = "ステータス：測色機が接続されていません。";
var locDeviceSerialNotFound1 = "ステータス：測色機のシリアル番号"; //"Status : Device Serial: {0} not found._ja";
var locDeviceSerialNotFound2 = "が見つかりません。"; //"Status : Device Serial: {0} not found._ja";
var locPleaseCalibrate = "ステータス：キャリブレーションを実施してください。";
var locPleaseMeasWhite = "白色キャリブレーションを行ってください。";
var locClickMeasureButton = "ステータス：「チャート測色」ボタンを押してください。";
var locNoScannerList = "測色機リストの取得に失敗しました:"; //"Failed to request scanner list: {0}_ja";
var locErrorText = "エラー";
var locSamplesCompleteText = "読み込みが完了しました";
var locReadSampleFailedText = "読み込みに失敗しました";
var locInvalidMeasurementDevice = "ステータス：ライセンスが無効な測色機です";


//i1read.js
var locStatusText = "ステータス："; //"Status : {0}_ja";
var locCalibrationSuccessAndClickMeasureButton = "ステータス：キャリブレーションが成功しました。「チャート測色」ボタンを押してください。";
var locPlaceCradlePressI1Trigger = "i1Proを白色基準タイルに置き、ボタンを押してください。";
var locCalibrationInProgress = "キャリブレーション中です。";
var locWhiteCalibrationSuccessAndClickMeasureButton = "紙白が測定されました。チャート測色ボタンをクリックして下ださい。";
var locCloseExactTrigger = "eXactをカラーチャートの紙白部分に置き、測定してください。";
var locWhiteCalibrationInProgress = "用紙を測定しています。";
var locStripReadInProgress = "チャートを読み込んでいます。";
var locPlaceScannerAndSlideAcrossStrip1 = "測色機をチャート";   //"Please place the scanner over the strip {0}.press and hold the trigger while sliding the scanner across the strip._ja";
var locPlaceScannerAndSlideAcrossStrip2 = "上にのせてください。チャート上をスライドさせる間、測定ボタンを押し続けてください。";
var locPlaceChartiOTable = "チャートをiOの台に置き、ウインドウ内をクリックしてください。";
var locMovei1VisorUpperLeftCorner = "i1Proをチャートの左上のパッチ上に移動し、i1Proのボタンを押してください。";
var locScanningDeviceUnpluggedWOStatusText = "測色機が接続されていません。";
var locMovei1VisorLowerLeftCorner = "i1Proをチャートの左下のパッチ上に移動し、i1Proのボタンを押してください。";
var locMovei1VisorLowerRightCorner = "i1Proをチャートの右下のパッチ上に移動し、i1Proのボタンを押してください。";
var locFailedReadExpectedSamples1 = "実際のパッチ数と、読み込んだパッチ数はが"; //"Failed to read expected number of samples {0} vs {1}_ja";
var locFailedReadExpectedSamples2 = "と";
var locFailedReadExpectedSamples3 = "で異なります。";
var locCannotReadCurrentColumn = "エラー：読み込み列の判定ができません。";
var locMeasurmentComplete = "ステータス：測色が完了しました。";
var locUploadMeasurementText = "測色結果のアップロード";
var locReadingProgress1 = "";  //"Reading progress {0} samples_ja";
var locReadingProgress2 = "個のパッチを読み込み中です";

//isisread.js
var locChartReadProgress = "チャートの読み込み中です";
var locPlaceChartScannerClickWindow = "チャートを測色機に設定し、ウインドウ内をクリックしてください。";
var locFailedReadExpectedSamplesError1 = "実際のパッチ数と、読み込んだパッチ数はが";  //"Failed to read expected number of samples({0} != {1})_ja";
var locFailedReadExpectedSamplesError2 = "と";  //"Failed to read expected number of samples({0} != {1})_ja";
var locFailedReadExpectedSamplesError3 = "で異なります。";  //"Failed to read expected number of samples({0} != {1})_ja";

//Printing.js
var locNoPrintInstalled = "ラベルプリンタが検出できませんでした。";
var locErrorWhilePrintingLabel = "ラベル印刷に失敗しました。再度印刷してください。";
var loc = "日本人_ja";

//Download Software
var locDownloadInstallMessage = "プラグインがインストールされていません。こちらからダウンロード、インストールしてください。";
var locDownloadPluginMessage = "インストールされたプラグインは古いバージョンです。新しいバージョンをインストールしてください。";
