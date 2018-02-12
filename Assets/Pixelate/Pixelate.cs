using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
[RequireComponent(typeof (Camera))]
[AddComponentMenu("Image Effects/Pixelate")]
public class Pixelate:MonoBehaviour{
	public Shader shader;
	int _pixelSizeX=1;
	int _pixelSizeY=1;
    int _clarityField = 0;
	Material _material;
	[Range(1,100)]
	public int pixelSizeX=1;
	[Range(1,100)]
	public int pixelSizeY=1;
	public bool lockXY=true;
    [Range(0, 100)]
    public int clarityField=0;
    void OnRenderImage(RenderTexture source, RenderTexture destination){
		if(_material==null) _material=new Material(shader);
		_material.SetInt("_PixelateX",pixelSizeX);
		_material.SetInt("_PixelateY",pixelSizeY);
        _material.SetInt("_PixelateField", clarityField);
        Graphics.Blit(source,destination,_material);
	}
	void OnDisable(){
		DestroyImmediate(_material);
	}

	void Update(){
		if(pixelSizeX!=_pixelSizeX){
			_pixelSizeX=pixelSizeX;
			if(lockXY) _pixelSizeY=pixelSizeY=_pixelSizeX;
		}
		if(pixelSizeY!=_pixelSizeY){
			_pixelSizeY=pixelSizeY;
			if(lockXY) _pixelSizeX=pixelSizeX=_pixelSizeY;
		}
        if (clarityField != _clarityField){
            _clarityField = clarityField;
        }
    }

}