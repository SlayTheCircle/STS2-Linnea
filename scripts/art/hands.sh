# 保留交付手势，按原版画布供齐四种联机贴图。
mkdir -p "$DST/hands"
for gesture in "${!HANDS[@]}"; do
    convert "$SRC/${HANDS[$gesture]}" -resize 422x1200 -background none -gravity north -extent 422x1200 "$DST/hands/${MOD_SHORT,,}_$gesture.png"
done
echo '四种联机手势生成完成。'
