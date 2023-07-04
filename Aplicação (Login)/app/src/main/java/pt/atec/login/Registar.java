package pt.atec.login;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.ProgressBar;

public class Registar extends AppCompatActivity {

    private ProgressBar Waiting_Registo;
    private Button Regista;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_registar);

        Waiting_Registo = findViewById(R.id.esperar_registo);
        Regista = findViewById(R.id.entrar);

        Waiting_Registo.setVisibility(View.INVISIBLE);

        Regista.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

            }
        });


    }
}
