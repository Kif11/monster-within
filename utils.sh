cmd=$1
app_name="com.codercat.monsterWithin"

case $cmd in
  "remote")
    shift
    # Configure remote adb for a connected device
    port=5555
    adb tcpip $port
    sleep 3
    device_ip=$(adb shell ip addr show wlan0 | grep "inet\s" | awk '{print $2}' | awk -F'/' '{print $1}')
    adb connect ${device_ip}:${port}
    ;;
  "log")
    shift
    # Listen for Unity log from Android device
    adb logcat -s Unity ActivityManager PackageManager dalvikvm DEBUG
    ;;
  "clear")
    shift
    # Remove project from android device
    adb uninstall $app_name
    ;;
  "upload")
    shift
    ovr-platform-util upload-mobile-build --app_id 3023686394315929 --app_secret `cat app-secret` --apk Builds/MonsterWithin_v0.3.apk --channel store --notes $1
    ;;
  *)
    echo "Usage: utils <cmd>"
    echo "Commands:"
    echo "  remote"
    echo "  log"
    echo "  clear"
    echo "  upload <comment>"
esac