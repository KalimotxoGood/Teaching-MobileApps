package com.example.cwt59.fastbreak;

import android.app.Service;
import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;

public class StartFast extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_start_fast);

    }

    public void BeginFast(View view){
        //Intent intent = new Intent( this, StartFast.class);
        // below is how to pass an intent for use in a Service to run in the backgroun
        Intent intent =new Intent(this, Service.class);

        //intent.putExtra("data", data); //adding the data


        this.startService(intent);

        //startActivity(intent);
    }


}
