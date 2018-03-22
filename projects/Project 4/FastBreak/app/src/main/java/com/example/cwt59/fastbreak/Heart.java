package com.example.cwt59.fastbreak;

import android.content.BroadcastReceiver;
import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.content.ServiceConnection;
import android.graphics.Color;
import android.os.IBinder;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.TextView;
import android.widget.Toast;

import static android.graphics.Color.*;

public class Heart extends AppCompatActivity {
    private static final String TAG = "com.cwt59.myTest";
    MyService caseysService;
    boolean isBound = false;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_heart);

        Intent i = new Intent(this,MyService.class);
        bindService(i, caseysConnection, Context.BIND_AUTO_CREATE);
        Log.i(TAG, "Water bind was reached");
    }

    private BroadcastReceiver br = new BroadcastReceiver() {


        @Override
        public void onReceive(Context context, Intent intent) {
            Toast.makeText(context, "heart broadcast", Toast.LENGTH_LONG).show();
            intent.getExtras();
            long millisUntilFinished = intent.getLongExtra("countdown", 0);
            double hoursLeft = millisUntilFinished/3600000;
            powerHeart(hoursLeft);
            //TextView tv = (TextView) findViewById(R.id.remainingView);
            //tv.setText(Long.toString(millisUntilFinished));


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

    private ServiceConnection caseysConnection = new ServiceConnection() {
        @Override
        public void onServiceConnected(ComponentName componentName, IBinder service) {
            MyService.MyLocalBinder binder = (MyService.MyLocalBinder) service;
            caseysService = binder.getService();
            Log.i(TAG, "Service connect reached!");
            isBound = true;

        }

        @Override
        public void onServiceDisconnected(ComponentName componentName) {
            isBound = false;
        }
    };


    public void powerHeart(double hourz){
        long time = caseysService.getHour();
        double ratio = hourz/time;
        Toast.makeText(this, Double.toString(hourz), Toast.LENGTH_LONG).show();
        TextView tv = (TextView) findViewById(R.id.heartView);
        if (ratio>.9){
            tv.setTextColor(Color.MAGENTA);
        }
        else if (ratio>.8){
            tv.setTextColor(Color.BLUE);
        }
        else if (ratio>.7){
            tv.setTextColor(Color.CYAN);
        }
        else if (ratio>.6){
            tv.setTextColor(Color.GREEN);
        }
        else if (ratio>.5){
            tv.setTextColor(Color.WHITE);
        }
        else if (ratio>.4){
            tv.setTextColor(Color.LTGRAY);
        }
        else if (ratio>.3){
            tv.setTextColor(Color.BLACK);
        }
        else if (ratio<.2){
            tv.setTextColor(Color.CYAN);
        }
        else if (ratio<.1){
            tv.setTextColor(Color.YELLOW);
        }
        else tv.setTextColor(Color.RED);





    }

}
