


---------------------------
■デファイン
---------------------------

---------------------------
UNITY_EDITOR
---------------------------
<Unity>
ゲームコードから Unity エディターのスクリプトを呼び出すための #define ディレクティブ

---------------------------
DEVELOPMENT_BUILD
---------------------------
<Unity>
Development Build オプションを有効にしてビルドしたプレイヤー

---------------------------
USE_DEF_FEE_DEBUGTOOL
---------------------------
<Fee>
デバッグログ

---------------------------
USE_DEF_FEE_INPUTSYSTEM
---------------------------
<Fee>
インプットシステム

以下から取得する必要がある
https://github.com/Unity-Technologies/InputSystem

---------------------------
USE_DEF_FEE_PUN
---------------------------
<Fee>
PUN

以下から取得する必要がある
https://assetstore.unity.com/packages/tools/network/pun-2-free-119922

---------------------------
USE_DEF_FEE_UNIVRM
---------------------------
<Fee>
UniVRM

以下から取得する必要がある
https://github.com/dwango/UniVRM

---------------------------
USE_DEF_FEE_UTF8JSON
---------------------------
<Fee>
Utf8Json

以下から取得する必要がある
https://github.com/neuecc/Utf8Json

以下の設定を変更する必要がある
PlayerSettings->OtherSettings->Configuration->ApiCompatibilityLevel = .NET4.x
PlayerSettings->OtherSettings->Configuration->All'unsafe'Code = true

---------------------------
■rspテンプレート
---------------------------
-define:NOUSE_DEF_FEE_DEBUGTOOL;NOUSE_DEF_FEE_INPUTSYSTEM;NOUSE_DEF_FEE_PUN;NOUSE_DEF_FEE_UNIVRM;NOUSE_DEF_FEE_UTF8JSON

