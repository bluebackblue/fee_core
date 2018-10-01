SET HOME=%~dp0
CD ..\..\
CALL setting.bat
CD %HOME%

%ANDROID_ADB% kill-server
@PAUSE

%ANDROID_ADB% devices
@PAUSE

%ANDROID_ADB% install -r -d fee.apk
@PAUSE