import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.io.File;

import javax.swing.JFrame;
import javax.swing.JProgressBar;
import javax.swing.Timer;

public class TreeExplorer implements Runnable {
    String root;
    JProgressBar progress_bar;
    int file_count = 0;
    JFrame frame;
    
    public TreeExplorer(String root, JProgressBar progress_bar, JFrame frame){
        this.root = root;
        this.progress_bar = progress_bar;
        this.frame = frame;
    }
    
    public void run(){
        Timer timer = new Timer(1000/60, new ActionListener() {
            public void actionPerformed(ActionEvent e) {
                progress_bar.setMaximum(file_count);
                frame.repaint();
            }
        });
        timer.start();
        explore(new File(root + "Patches\\"));
        explore(new File(root + "Defs\\"));
        timer.stop();
        progress_bar.setMaximum(file_count);
        System.out.println(file_count + " Files");
    }
    
    private void explore(File f){
        if(f.isDirectory()){
            for(File f1: f.listFiles()){
                if(f1.isDirectory()){
                    explore(f1);
                }else if(f1.getName().endsWith(".xml")){
                    file_count++;
                }
            }
        }else if(f.getName().endsWith(".xml")){
            file_count++;
        }
    }
}
