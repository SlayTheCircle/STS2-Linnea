# 角色主题能量球母版派生两种图标；白色火花/辉光由场景施加统一主题色。
mkdir -p "$DST/energy"
convert -size 64x64 xc:white -alpha set \
    -channel A -fx 'max(0,1-hypot(i-31.5,j-31.5)/31.5)^2' +channel \
    "$DST/energy/linnea_spark.png"
convert -size 128x128 xc:white -alpha set \
    -channel A -fx 'max(0,1-hypot(i-63.5,j-63.5)/63.5)^2' +channel \
    "$DST/energy/linnea_glow.png"
convert "$SRC/$ENERGY_SOURCE" -resize 256x256 -background none -gravity center -extent 256x256 \
    "$DST/energy/linnea_energy_big.png"
convert "$DST/energy/linnea_energy_big.png" -resize 24x24 "$DST/energy/linnea_energy_text.png"
echo '能量球: 正式母版 big/text 与能量计/拖尾柔光派生完成'
