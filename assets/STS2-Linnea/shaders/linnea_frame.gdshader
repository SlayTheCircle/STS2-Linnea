shader_type canvas_item;

// Palette B: lift the vanilla red relief independently of the neutral text panel.
uniform vec4 theme_color : source_color = vec4(0.9568627, 0.7294118, 0.8039216, 1.0);
varying vec4 modulate_color;

void vertex() {
    modulate_color = COLOR;
}

void fragment() {
    vec4 frame = texture(TEXTURE, UV);
    float relief = dot(frame.rgb, vec3(0.299, 0.587, 0.114));
    float rim = smoothstep(0.10, 0.28, frame.r - max(frame.g, frame.b));
    float bevel = clamp((relief - 0.20) * 2.6 + 0.60, 0.25, 1.06);
    vec3 panel = relief * mix(vec3(1.0), theme_color.rgb, 0.45);
    vec3 raised = mix(panel, theme_color.rgb * bevel, rim);
    COLOR = vec4(raised, frame.a * theme_color.a) * modulate_color;
}
