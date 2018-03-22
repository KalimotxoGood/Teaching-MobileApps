package com.example.cwt59.fastbreak;

import android.app.Service;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.nfc.Tag;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.util.Log;
import android.widget.Button;
import android.widget.TextView;


public class StartFast extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_start_fast);


    }

    public void startService(View view){

        Intent intent  = new Intent(this, MyService.class);
        startService(intent);
    }

    //Method to stop the service

    public void stopService(View view){
        TextView tv = findViewById(R.id.timeView1);

        stopService(new Intent(getBaseContext(), MyService.class));
    }


}

