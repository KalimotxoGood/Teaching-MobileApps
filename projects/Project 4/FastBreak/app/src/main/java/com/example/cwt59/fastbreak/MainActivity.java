package com.example.cwt59.fastbreak;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;

public class MainActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
    }

    public void StartFast(View view){
        Intent intent = new Intent( this, StartFast.class);


        startActivity(intent);
    }

    public void SeeWater(View view){
        Intent intent = new Intent(this, WaterGauge.class);

        startActivity(intent);

    }

    public void SeeHeart(View view){
        Intent intent = new Intent(this, Heart.class);


        startActivity(intent);

    }

    public void SeeMotivate(View view){
        Intent intent = new Intent(this, Motivated.class);

        startActivity(intent);

    }
}

