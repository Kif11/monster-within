cmd=$1
version="0.6"
app_name="com.codercat.monsterWithin"
apk="./Builds/MonsterWithin_v${version}.apk"
release_notes="ReleaseNotes_v${version}.txt"

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
  "keygen")
    shift
    # Generate a key for code signing
    keytool -genkey -alias kif -keyalg RSA -keysize 2048 -validity 365000 -keystore ~/.keystore/
    ;;
  "sign")
    shift
    # Sign apk for the Oculus store
    zip -d $apk 'META-INF/*.SF' 'META-INF/*.RSA' # Unsign Unity debug signing
    jarsigner -verbose -sigalg SHA1withRSA -digestalg SHA1 -keystore ~/.android/kif.keystore $apk kif
    ;;
  "sign_verify")
    shift
    # Verify apk signature
    jarsigner -verify -verbose -certs $apk
    ;;
  "upload")
    shift
    ovr-platform-util upload-mobile-build --app_id 3023686394315929 --app_secret `cat app-secret` --apk $apk --channel store --notes "`cat $release_notes`"
    ;;
  *)
    echo "Usage: utils <cmd>"
    echo "Commands:"
    echo "  remote"
    echo "  log"
    echo "  clear"
    echo "  keygen"
    echo "  sign"
    echo "  sign_verify"
    echo "  upload <comment>"
esac