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
import android.widget.TextView;


public class StartFast extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_start_fast);
        startService(new Intent(this, MyService.class));
        Log.i("Started service", "hello started service...");
        registerReceiver(br, new IntentFilter("COUNTDOWN_UPDATED"));
    }

    private BroadcastReceiver br = new BroadcastReceiver() {
        @Override
        public void onReceive(Context context, Intent intent) {
            intent.getExtras();
            long millisUntilFinished = intent.getLongExtra("countdown",0);
            String time = Long.toString((millisUntilFinished));
            TextView tv = findViewById(R.id.timeView1);
            tv.setText(time);
        }
    };

    public void BeginFast(View view){
        //Intent intent = new Intent( this, StartFast.class);
        // below is how to pass an intent for use in a Service to run in the backgroun
       Intent intent =new Intent(this, MyService.class);
       startService(intent);
         //   intent.putExtra() // putExtra longs ...will do after static run succeeds
        //intent.putExtra("data", data); //adding the data

        Intent intent1 = new Intent(this, Heart.class);
        startActivity(intent1);
    }


}
