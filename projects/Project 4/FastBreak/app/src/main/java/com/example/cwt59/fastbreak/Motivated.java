package com.example.cwt59.fastbreak;

import android.content.BroadcastReceiver;
import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.content.ServiceConnection;
import android.os.IBinder;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.TextView;
import android.widget.Toast;

public class Motivated extends AppCompatActivity {
    private static final String TAG = "com.cwt59.myTest";
    MyService caseysService;
    boolean isBound = false;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_motivated);

    }

    private BroadcastReceiver br = new BroadcastReceiver() {


        @Override
        public void onReceive(Context context, Intent intent) {
            //Toast.makeText(context, "Broadcast received", Toast.LENGTH_LONG).show();
            intent.getExtras();
            long millisUntilFinished = intent.getLongExtra("countdown", 0);
            double hoursLeft = (double)millisUntilFinished/3600000;
            TextView tv = (TextView) findViewById(R.id.remainingView);
            tv.setText(Double.toString(hoursLeft)+" hours Left. You got this!");

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

    @Override
    public void onBackPressed()
    {
        // code here to show dialog
      //  onSaveInstanceState(getBaseContext())
        super.onBackPressed();  // optional depending on your needs
    }


    public void motivateButton(View view){

        Intent intent = new Intent(this, Heart.class);
        startActivity(intent);

    }

}
