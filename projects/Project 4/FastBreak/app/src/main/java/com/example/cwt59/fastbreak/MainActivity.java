package com.example.cwt59.fastbreak;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Toast;

public class MainActivity extends AppCompatActivity {

    private static final String TAG = "com.cwt59.myTest";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
    }
    //Start the Broadcast Receiver to get the time remaining
    private BroadcastReceiver br = new BroadcastReceiver() {


        @Override
        public void onReceive(Context context, Intent intent) {
            Toast.makeText(context, "Broadcast received", Toast.LENGTH_LONG).show();
            intent.getExtras();
            long millisUntilFinished = intent.getLongExtra("countdown", 0);
            Toast.makeText(context, Long.toString(millisUntilFinished), Toast.LENGTH_LONG).show();

        }
    };

    @Override
    public void onResume() {
        super.onResume();
        registerReceiver(br, new IntentFilter(MyService.COUNTDOWN_BR));
        Log.i(TAG, "Registered broacast receiver");
    }

    @Override
    public void onPause() {
        super.onPause();
        unregisterReceiver(br);
        Log.i(TAG, "Unregistered broacast receiver");
    }

    @Override
    public void onStop() {
        try {
            unregisterReceiver(br);
        } catch (Exception e) {
            // Receiver was probably already stopped in onPause()
        }
        super.onStop();
    }
    @Override
    public void onDestroy() {
        stopService(new Intent(this, MyService.class));
        Log.i(TAG, "Stopped service");
        super.onDestroy();
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

