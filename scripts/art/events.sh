# 按原稿母版生成完整事件图窗；只改派生尺寸，不写回母版。
mkdir -p "$DST/events"
for path in "${!EVENTS[@]}"; do
    cls="${EVENTS[$path]}"
    convert "$SRC/$path" -resize 1672x941^ -gravity center -extent 1672x941 "$DST/events/$cls.png"
done
echo "事件肖像: ${#EVENTS[@]} 张"
