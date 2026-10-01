shader_type canvas_item;

// Reads the screen buffer captured by BackBufferCopy
uniform sampler2D screen_texture : hint_screen_texture, filter_nearest;

void fragment() {
    COLOR = texture(screen_texture, SCREEN_UV);
}