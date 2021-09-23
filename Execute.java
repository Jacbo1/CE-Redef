import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.PrintWriter;

import org.ini4j.Ini;

public class Execute implements Runnable {
    private String[] tags = new String[] {"Bulk", "CarryBulk", "WornBulk", "ReloadSpeed", "reloadTime", "RangedWeapon_Cooldown", "cooldownTime", "warmupTime"};
    private String ce_dir, backup_path, mode;
    private double bulk_mult = -1234,
        carry_bulk_mult = -1234,
        worn_bulk_mult = -1234,
        reload_speed_mult = -1234,
        reload_time_mult = -1234,
        ranged_cooldown_mult = -1234,
        melee_cooldown_mult = -1234,
        warmup_mult = -1234;
    int file_index = 0;
    boolean running = true;
    private String version;
    
    public Execute(String version,
        Ini config, String ce_dir,
        String backup_path,
        String mode,
        double bulk_mult,
        double carry_bulk_mult,
        double worn_bulk_mult,
        double reload_speed_mult,
        double reload_time_mult,
        double ranged_cooldown_mult,
        double melee_cooldown_mult,
        double warmup_mult){
        this.version = version;
        this.ce_dir = ce_dir;
        this.backup_path = backup_path;
        this.mode = mode;
        this.bulk_mult = bulk_mult;
        this.carry_bulk_mult = carry_bulk_mult;
        this.worn_bulk_mult = worn_bulk_mult;
        this.reload_speed_mult = reload_speed_mult;
        this.reload_time_mult = reload_time_mult;
        this.ranged_cooldown_mult = ranged_cooldown_mult;
        this.melee_cooldown_mult = melee_cooldown_mult;
    }
    
    public void run() {
        switch(mode){
            case "backup":
                backup();
            break;
            
            case "restore":
                restore();
            break;
            
            case "redefine":
                redefine();
            break;
        }
        running = false;
        System.out.println("Finished");
    }
    
    private void backup() {
        try{
            System.out.println("Backing up");
            new File(removeFileName(backup_path)).mkdirs();
            new File(backup_path).delete();
            new File(backup_path).createNewFile();
            Ini backup = new Ini(new File(backup_path));
            if(version != null){
            	backup.put("version", "version", version);
            }
            backup(backup, new File(ce_dir + "Patches\\"), "Patches/");
            backup(backup, new File(ce_dir + "Defs\\"), "Defs/");
            backup.store();
        }catch(Exception e){
            e.printStackTrace();
        }
    }
    
    private void backup(Ini ini, File f, String path){
        if(f.isDirectory()){
            String new_path = path + f.getName() + "/";
            for(File f1: f.listFiles()){
                backup(ini, f1, new_path);
            }
        }else{
            String name = f.getName();
            if(name.endsWith(".xml")){
                // Valid file
                String section = path + name.substring(0, name.length() - 4);
                int entry = 0;
                
                String data = readFile(f.getAbsolutePath());
                for(String tag: tags){
                    String open_tag = "<" + tag + ">";
                    String close_tag = "</" + tag + ">";
                    int open_len = open_tag.length();
                    int open = 0;
                    int close = 0;
                    int offset_open = data.indexOf("<equippedStatOffsets>");
                    int offset_close = (offset_open == -1 ? -1 : data.indexOf("</equippedStatOffsets>", offset_open));
                    while(open != -1 && close != -1){
                        try{
                            open = data.indexOf(open_tag, close);
                            close = data.indexOf(close_tag, open);
                            if(offset_close != -1){
                                boolean cont = false;
                                boolean reloop = true;
                                do{
                                    if(open < offset_close){
                                        reloop = false;
                                        if(open > offset_open){
                                            cont = true;
                                            break;
                                        }
                                    }else{
                                        reloop = true;
                                        offset_open = data.indexOf("<equippedStatOffsets>", offset_close);
                                        offset_close = (offset_open == -1 ? -1 : data.indexOf("</equippedStatOffsets>", offset_open));
                                    }
                                }while(reloop && offset_close != -1);
                                if(cont){
                                    break;
                                }
                            }
                            if(open != -1 && close != -1){
                                open += open_len;
                                entry++;
                                ini.put(section, entry+"", data.substring(open, close).trim());
                            }
                        }catch(Exception e){}
                    }
                }
                
                file_index++;
            }
        }
    }
    
    private void restore() {
        try{
            System.out.println("Restoring");
            Ini backup = new Ini(new File(backup_path));
            restore(backup, new File(ce_dir + "Patches\\"), "Patches/");
            restore(backup, new File(ce_dir + "Defs\\"), "Defs/");
        }catch(Exception e){
            e.printStackTrace();
        }
    }
    
    private void restore(Ini ini, File f, String path) throws Exception {
        if(f.isDirectory()){
            String new_path = path + f.getName() + "/";
            for(File f1: f.listFiles()){
                restore(ini, f1, new_path);
            }
        }else{
            String name = f.getName();
            if(name.endsWith(".xml")){
                // Valid file
                String section = path + name.substring(0, name.length() - 4);
                int entry = 0;
                
                String abs_path = f.getAbsolutePath();
                String data = readFile(abs_path);
                boolean changed = false;
                for(String tag: tags){
                    String open_tag = "<" + tag + ">";
                    String close_tag = "</" + tag + ">";
                    int open_len = open_tag.length();
                    int open = 0;
                    int close = 0;
                    String val;
                    int offset_open = data.indexOf("<equippedStatOffsets>");
                    int offset_close = (offset_open == -1 ? -1 : data.indexOf("</equippedStatOffsets>", offset_open));
                    while(open != -1 && close != -1){
                        try{
                            open = data.indexOf(open_tag, close);
                            close = data.indexOf(close_tag, open);
                            if(offset_close != -1){
                                boolean cont = false;
                                boolean reloop = true;
                                do{
                                    if(open < offset_close){
                                        reloop = false;
                                        if(open > offset_open){
                                            cont = true;
                                            break;
                                        }
                                    }else{
                                        reloop = true;
                                        offset_open = data.indexOf("<equippedStatOffsets>", offset_close);
                                        offset_close = (offset_open == -1 ? -1 : data.indexOf("</equippedStatOffsets>", offset_open));
                                    }
                                }while(reloop && offset_close != -1);
                                if(cont){
                                    break;
                                }
                            }
                            if(open != -1 && close != -1){
                                open += open_len;
                                entry++;
                                val = ini.get(section, entry+"");
                                if(val != null){
                                    changed = true;
                                    data = data.substring(0, open) + val + data.substring(close);
                                }
                            }
                        }catch(Exception e){}
                    }
                }
                
                if(changed){
                    save(abs_path, data);
                }
                
                file_index++;
            }
        }
    }
    
    private void redefine() {
        try{
            System.out.println("Repatching");
            double[] mults = new double[] {bulk_mult, carry_bulk_mult, worn_bulk_mult, reload_speed_mult, reload_time_mult, ranged_cooldown_mult, melee_cooldown_mult, warmup_mult};
            Ini backup = new Ini(new File(backup_path));
            redefine(backup, "Patches/", new File(ce_dir + "Patches\\"), mults);
            redefine(backup, "Defs/", new File(ce_dir + "Defs\\"), mults);
        }catch(Exception e){
            e.printStackTrace();
        }
    }
    
    private void redefine(Ini backup, String path, File f, double[] mults) throws Exception {
        if(f.isDirectory()){
            String new_path = path + f.getName() + "/";
            for(File f1: f.listFiles()){
                redefine(backup, new_path, f1, mults);
            }
        }else{
            String name = f.getName();
            if(name.endsWith(".xml")){
                // Valid file
                String section = path + name.substring(0, name.length() - 4);
                int entry = 0;
                
                String abs_path = f.getAbsolutePath();
                String data = readFile(abs_path);
                boolean changed = false;
                int index = 0;
                
                for(String tag: tags){
                    double mult = mults[index];
                    index++;
                    String open_tag = "<" + tag + ">";
                    String close_tag = "</" + tag + ">";
                    int open_len = open_tag.length();
                    int open = 0;
                    int close = 0;
                    int offset_open = data.indexOf("<equippedStatOffsets>");
                    int offset_close = (offset_open == -1 ? -1 : data.indexOf("</equippedStatOffsets>", offset_open));
                    while(open != -1 && close != -1){
                        try{
                            open = data.indexOf(open_tag, close);
                            close = data.indexOf(close_tag, open);
                            if(offset_close != -1){
                                boolean cont = false;
                                boolean reloop = true;
                                do{
                                    if(open < offset_close){
                                        reloop = false;
                                        if(open > offset_open){
                                            cont = true;
                                            break;
                                        }
                                    }else{
                                        reloop = true;
                                        offset_open = data.indexOf("<equippedStatOffsets>", offset_close);
                                        offset_close = (offset_open == -1 ? -1 : data.indexOf("</equippedStatOffsets>", offset_open));
                                    }
                                }while(reloop && offset_close != -1);
                                if(cont){
                                    break;
                                }
                            }
                            if(open != -1 && close != -1){
                                open += open_len;
                                entry++;
                                double val = backup.get(section, entry+"", double.class);
                                changed = true;
                                data = data.substring(0, open) + val*mult + data.substring(close);
                            }
                        }catch(Exception e){}
                    }
                }
                
                if(changed){
                    save(abs_path, data);
                }
                
                file_index++;
            }
        }
    }
    
    private String readFile(String path){
        String content = "";
        File f = new File(path);
        if(f.exists()){
            FileReader fr = null;
            BufferedReader br = null;
            try{
                fr = new FileReader(path);
                br = new BufferedReader(fr);
                int i;
                while((i=br.read())!=-1)
                    content += (char)i;
            }catch(Exception e){System.out.println(e);}
            finally{
                try{
                    if(fr!=null)fr.close();
                    if(br!=null)br.close();
                    fr = null;
                    br = null;
                }catch(Exception E){System.out.println(E);}
            }
        }
        return content;
    }
    
    public void save(String file, String text){
        try{
            new File(file).delete();
            PrintWriter out = new PrintWriter(new File(file));
            out.print(text);
            out.close();
        }catch(Exception e){
            e.printStackTrace();
        }
    }
    
    private String removeFileName(String path){
        return path.substring(0, path.lastIndexOf('\\')+1);
    }
}
